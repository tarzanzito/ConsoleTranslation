using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTranslation
{
    internal sealed class TranslatorHelper
    {
        private static readonly char[] CharsToSplitAtEndDefault = new char[] { '.', '?', '!' };

        private ITranslator _translator;
        private int _blockSize;
        private bool _useNewLineToSplitAtEnd = true;
        private char[] _charsToSplitAtEnd = new[] { '\0' };
        private bool _findOnlyByOneChar;

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

        public TranslatorHelper(ITranslator translator)
        {
            _translator = translator;
            _blockSize = _translator.MaxTextLengthCanSend;
            _useNewLineToSplitAtEnd = true;
            CharsToSplitAtEnd = CharsToSplitAtEndDefault;
        }

        public TranslatorHelper(ITranslator translator, char[] charsToSplitAtEnd)
        {
            _translator = translator;
            _blockSize = _translator.MaxTextLengthCanSend;
            _useNewLineToSplitAtEnd = false;
            CharsToSplitAtEnd = charsToSplitAtEnd;
        }

        public async Task<string> GenerateStringFromBlockListAsync(List<string> blockList)
        {
            await Task.Yield();

            ValidateBlockList(blockList);

            StringBuilder stringBuilder = new();

            foreach (string item in blockList)
                stringBuilder.Append(item);

            return stringBuilder.ToString();
        }

        public async Task<List<string>> GenerateBlockListFromStringAsync(string text)
        {
            ValidateText(text);
            ValidationBlockSize();

            List<string> sourceBlockList = await CreateBlockListAsync(text);

            return sourceBlockList;
        }


        private async Task<List<string>> CreateBlockListAsync(string text)
        {
            await Task.Yield();

            bool findOnlyOneChar = _charsToSplitAtEnd.Length == 1;
            int pos = 0;
            string temp = string.Empty;
            int maxBlockSize = 0;
            int realLength = 0;
            List<string> blockList = new();

            try
            {
                while (pos < text.Length)
                {
                    //if (blockList.Count > 3)
                    //    Console.WriteLine("xxxxxxxxxx");

                    //if ((pos + blockSize) < text.Length)
                    //    maxBlockSize = (pos + blockSize);
                    //else
                    //{
                    //    maxBlockSize = text.Length;
                    //    isLast = true;
                    //}

                    // 1. Define o tamanho máximo que podemos ler a partir da posição atual
                    maxBlockSize = Math.Min(pos + _blockSize, text.Length); // - pos);

                    // 2. Tira o pedaço provisório (a nova string a analisar)
                    temp = text.Substring(pos, maxBlockSize - pos);

                    // 3. Procura o últimofindOneChar trás para a frente dentro deste pedaço

                    if (maxBlockSize >= text.Length)
                        realLength = text.Length;
                    else
                    {
                        if (_useNewLineToSplitAtEnd)
                        {
                            realLength = temp.LastIndexOf(System.Environment.NewLine);
                        }
                        else
                        {
                            if (findOnlyOneChar)
                                realLength = temp.LastIndexOf(_charsToSplitAtEnd);
                            else
                                realLength = temp.LastIndexOfAny(_charsToSplitAtEnd);
                        }
                    }

                    // Se encontrou um ponto E não é o último bloco completo do texto
                    if (realLength != -1 && (pos + maxBlockSize) < text.Length)
                    {
                        // O tamanho real do corte será até ao ponto (incluindo o ponto)
                        //int realLength = lastPos;// + 1;

                        // Recorta a string final com o tamanho corrigido
                        string pedacoFinal = temp.Substring(0, realLength);
                        blockList.Add(pedacoFinal);

                        // O loop avança exatamente para a posição a seguir ao ponto
                        pos += realLength;
                    }
                    else
                    {
                        // Se não houver ponto (ou se for o fim do texto), aceita o pedaço inteiro
                        blockList.Add(temp);
                        pos += temp.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


        
            //return Task.CompletedTask; //if return is void blockList;
            //return Task.FromResult(blocos);  parece que não que async na defenição

            return blockList;
        }

        //private async Task<List<string>> TranslateBlockListAsync(List<string> sourceList, string sourceLang, string targtLang)
        //{
        //    List<string> targetList = new();

        //    foreach (string item in sourceList)
        //    {
        //        string text = await _translatator.TranslateAsync(item, sourceLang, targtLang);
        //        targetList.Add(text);
        //    }

        //    return targetList;
        //}

        public async Task<string> TranslateTextAsync(string shortText, string sourceLang, string targetLang)
        {
            string translatedText = await _translator.TranslateAsync(shortText, sourceLang, targetLang);

            return translatedText;
        }

        public async Task<List<string>> TranslateBlockListAsync(List<string> blockList, string sourceLang, string targetLang)
        {
            ValidateBlockList(blockList);

            List<int> valueList = new();
            foreach (string item in blockList)
            {
                valueList.Add(item.Length);
            }

            bool validateLangs = true;
            List<string> translatedList = new();

            foreach (string item in blockList)
            {
                ValidateText(item);

                if (validateLangs)
                {
                    ValidateLanguages(sourceLang, targetLang);
                    validateLangs = false;
                }

                string translatedText = await _translator.TranslateAsync(item, sourceLang, targetLang);
                translatedList.Add(translatedText);

            }

            return translatedList;
        }

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

            if (text.Length > _blockSize)
                throw new Exception($"Text length is {text.Length} but cannot be longer than [{_blockSize}.");
        }

        private void ValidateLanguages(string sourceLang, string targtLang)
        {
            if (string.IsNullOrEmpty(sourceLang))
                throw new Exception("sourceLang is null or empty.");

            if (string.IsNullOrEmpty(targtLang))
                throw new Exception("targtLang is null or empty.");

            //if (charsToFind is null || charsToFind.Length == 0)
            //    throw new Exception("char[] charsToFind is empty.");
        }
    }
}
