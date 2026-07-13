using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleTranslation
{


    internal class Class1
    {
        private static readonly char[] charsToFindDefault = new char[] { '.', '?', '!', '\n' };

        ITranslatator _translatator;

        public Class1(ITranslatator translatator)
        {
            _translatator = translatator;
        }

        public async Task<string> TranslateAsync(string text, string sourceLang, string targetLang, char[] charsToFind, int blockSize)
        {
            Validation(text, sourceLang, targetLang, charsToFind, blockSize);

            List<string> sourceBlockList = await CreateBlockListAsync(text, charsToFind, blockSize);

            List<string> targetBlockList = await TranslateBlockListAsync(sourceBlockList, sourceLang, targetLang);

            StringBuilder stringBuilder = new();
            
            foreach (string item in targetBlockList)
            {
                string translatedText = await _translatator.TranslateAsync(item, sourceLang, targetLang);
                stringBuilder.Append(translatedText);
            }
            return stringBuilder.ToString();
        }

        private void Validation(string text, string sourceLang, string targtLang, char[] charsToFind, int blockSize)
        {
            if (string.IsNullOrEmpty(text))
                throw new Exception("Text is null or empty.");

            if (string.IsNullOrEmpty(sourceLang))
                throw new Exception("sourceLang is null or empty.");

            if (string.IsNullOrEmpty(targtLang))
                throw new Exception("targtLang is null or empty.");

            if (blockSize <= 1)
                throw new Exception("Block size must by greatter than one.");

            if (charsToFind is null || charsToFind.Length == 0)
                throw new Exception("char[] charsToFind is empty.");
        }

        private async Task<List<string>> CreateBlockListAsync(string text, char[] charsToFind, int blockSize)
        {
            await Task.Yield();

            bool findOneChar = charsToFind.Length == 1;
            List<string> blockList = new();
            int pos = 0;
  
            while (pos < text.Length)
            {
                // 1. Define o tamanho máximo que podemos ler a partir da posição atual
                int maxBlockSize = Math.Min(pos + blockSize, text.Length - pos);

                // 2. Tira o pedaço provisório (a nova string a analisar)
                string temp = text.Substring(pos, maxBlockSize);

                // 3. Procura o últimofindOneChar trás para a frente dentro deste pedaço
                int lastPos = 0;
                if (findOneChar)
                     lastPos = temp.LastIndexOf(charsToFind);
                else
                     lastPos = temp.LastIndexOfAny(charsToFind);

                // Se encontrou um ponto E não é o último bloco completo do texto
                if (lastPos != -1 && (pos + maxBlockSize) < text.Length)
                {
                    // O tamanho real do corte será até ao ponto (incluindo o ponto)
                    int realLength = lastPos + 1;

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
                    pos += maxBlockSize;
                }
            }

            //return Task.CompletedTask; //if return is void blockList;
            //return Task.FromResult(blocos);  parece que não que async na defenição
            return blockList;
        }

        private async Task<List<string>> TranslateBlockListAsync(List<string> sourceList, string sourceLang, string targtLang)
        {
            List<string> targetList = new();

            foreach (string item in sourceList)
            {
                string text = await _translatator.TranslateAsync(item, sourceLang, targtLang);
                targetList.Add(text);
            }

            return targetList;
        }
    }
}
