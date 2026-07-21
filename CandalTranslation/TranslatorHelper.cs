using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace Candal.Translation
{
    public abstract class TranslatorHelper
    {
        #region Fields

        private static readonly char[] CharsToSplitAtEndDefault = new char[] { '.', '?', '!' };

        private ITranslator _translator;
        private int _blockSize;
        private bool _useNewLineToSplitAtEnd;
        private char[] _charsToSplitAtEnd = new[] { '\0' };
        private bool _findOnlyByOneChar;

        #endregion

        #region Properties

        public bool UseNewLineToSplitAtEnd
        {
            get
            {
                return _useNewLineToSplitAtEnd;
            }
            set
            {
                _useNewLineToSplitAtEnd = value;
            }
        }

        public char[] CharsToSplitAtEnd
        { 
            get
            {
                return _charsToSplitAtEnd;
            }
            set
            {
                _charsToSplitAtEnd = value;
                //if (_charsToSplitAtEnd != null)
                _findOnlyByOneChar = (_charsToSplitAtEnd.Length == 1);
            }
        }

        #endregion

        #region Constructors

        public TranslatorHelper(ITranslator translator) : this(translator, new[] { '\0' })
        {
            UseNewLineToSplitAtEnd = true;
        }

        public TranslatorHelper(ITranslator translator, char[] charsToSplitAtEnd)
        {
            _translator = translator;
            if (translator == null)
                throw new Exception("Parameter 'translator' is null.");

            _blockSize = _translator.MaxTextLengthCanSend;
            //UseNewLineToSplitAtEnd = false;  is default value of bool

            if (charsToSplitAtEnd == null)
                throw new Exception("Parameter 'charsToSplitAtEndText' is null.");

            if (charsToSplitAtEnd.Length > 0)
                CharsToSplitAtEnd = charsToSplitAtEnd;
            else
                throw new Exception("Parameter 'charsToSplitAtEndText.length' is less than 1.");
        }

        #endregion

        #region Public Methods

        public abstract Task<List<string>> CreateBlockListFromStringAsync(string text, CancellationToken cancellationToken = default);
        public abstract Task<string> CreateStringFromBlockList(List<string> blockList, CancellationToken cancellationToken = default);


        protected string GenerateStringFromBlockList(List<string> blockList, CancellationToken cancellationToken = default)
        {
            string result = string.Empty;

            try
            {
                ValidateBlockList(blockList);

                StringBuilder stringBuilder = new();

                foreach (string item in blockList)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    stringBuilder.Append(item);
                }

                result = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }



        #endregion

        protected List<string> GenerateBlockListFromString(string text, CancellationToken cancellationToken = default)
        {
            List<string> blockList = new List<string>();

            try
            {
                ValidateText(text);
                ValidationBlockSize();

                if (string.IsNullOrEmpty(text))
                    return blockList;

                int currentPos = 0;
                int textLength = text.Length;

                while (currentPos < textLength)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // Se o texto restante for menor que o limite, adiciona tudo e termina
                    if ((currentPos + _blockSize) >= textLength)
                    {
                        blockList.Add(text.Substring(currentPos));
                        break;
                    }

                    // Obtém o bloco inicial de 5000 caracteres
                    string tempBlock = text.Substring(currentPos, _blockSize);

                    // Procura a última ocorrência de quebra de linha dentro deste bloco
                    int lastPos = FindLastControlCharsPosition(tempBlock);

                    if (lastPos != -1)
                    {
                        // Corta até à quebra de linha (incluindo o caractere \n)
                        int newPos = lastPos + 1;
                        blockList.Add(tempBlock.Substring(0, newPos));
                        currentPos += newPos;
                    }
                    else
                    {
                        // Se não houver quebra de linha, corta nos 5000 caracteres exatos
                        blockList.Add(tempBlock);
                        currentPos += _blockSize;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return blockList;
        }

        //private async Task<string> GenerateStringFromBlockListAsync(List<string> blockList, CancellationToken cancellationToken = default)
        //{
        //    string result = string.Empty;

        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        result = await Task.Run(() => GenerateStringFromBlockList(blockList), cancellationToken),cancellationToken);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw;
        //    }

        //    return result;
        //}

        

        public async Task<string> TranslateTextAsync(string text, string sourceLang, string targetLang, CancellationToken cancellationToken = default)
        {
            string translatedText = string.Empty;

            try
            {
                ValidateText(text);
                ValidateLanguages(sourceLang, targetLang);

                translatedText = await _translator.TranslateAsync(text, sourceLang, targetLang, cancellationToken);
            }
            catch(Exception ex)
            {
                throw;
            }

            return translatedText;
        }

        public async Task<List<string>> TranslateBlockListAsync(List<string> blockList, string sourceLang, string targetLang, CancellationToken cancellationToken = default)
        {
            List<string> translatedList = new();

            try
            {
                //cancellationToken.ThrowIfCancellationRequested();

                ValidateBlockList(blockList);

                bool validateLangs = true;

                foreach (string item in blockList)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    ValidateText(item);

                    if (validateLangs)
                    {
                        ValidateLanguages(sourceLang, targetLang);
                        validateLangs = false;
                    }

                    string translatedText = await _translator.TranslateAsync(item, sourceLang, targetLang, cancellationToken);

                    translatedList.Add(translatedText);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return translatedList;
        }

        #region Private Methods

        private void ValidationBlockSize()
        {
            if (_blockSize <= 1)
                throw new Exception("Block size must by greatter than one.");
        }

        private void ValidationCharsToSplitAtEnd()
        {
            if (!_useNewLineToSplitAtEnd)
            {
                if (_charsToSplitAtEnd is null || _charsToSplitAtEnd.Length == 0)
                    throw new Exception("char[] charsToSplitAtEnd is empty.");
            }
        }

        private void ValidateBlockList(List<string> blockList)
        {
            if (blockList == null)
                throw new Exception("blockList is null.");
        }

        private void ValidateText(string text)
        {
            if (text == null)
                throw new Exception("text is null.");
        }

        private void ValidateLanguages(string sourceLang, string targtLang)
        {
            if (string.IsNullOrEmpty(sourceLang))
                throw new Exception("sourceLang is null or empty.");

            if (string.IsNullOrEmpty(targtLang))
                throw new Exception("targtLang is null or empty.");
        }
    
        private int FindLastControlCharsPosition(string text)
        {
            int lastPos = text.Length;

            if (_useNewLineToSplitAtEnd)
                lastPos = text.LastIndexOf(System.Environment.NewLine);
            else
            {
                if (_findOnlyByOneChar)
                    lastPos = text.LastIndexOf(_charsToSplitAtEnd);
                else
                    lastPos = text.LastIndexOfAny(_charsToSplitAtEnd);
            }

            return lastPos;
        }

        #endregion
    }
}
