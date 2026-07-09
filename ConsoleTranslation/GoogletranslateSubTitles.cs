using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace ConsoleTranslation
{
    internal class GoogletranslateSubTitles
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string URL = "https://translate.googleapis.com/translate_a/single";
        private const int BLOCK_SIZE = 4500;

        public async Task<string> TranslateSubTitles(string text, string sourceLang, string targetLang)
        {
            var finalResult = new StringBuilder();

            var tempLine = new StringBuilder();
            int totalLen = 0;

            // 1. splt by  NEWLINE
            var lineArray = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            //com rest api by line
            foreach (string line in lineArray)
            {
                // Mantem linhas vazias pra não estragar o tempo da legenda
                if (string.IsNullOrWhiteSpace(line))
                {
                    finalResult.AppendLine();
                    continue;
                }

                if (line.Length >= BLOCK_SIZE)
                {
                    // Se a linha for muito grande, aí sim corta por tamanho
                    await TranslatorBigText(line, sourceLang, targetLang, finalResult);
                    continue;
                }

                totalLen += line.Length;
                if (totalLen < BLOCK_SIZE)
                {
                    tempLine.AppendLine(line);
                    continue;
                }


                string tempText = tempLine.ToString();
                string result = await TranslatorAsync(tempText, sourceLang, targetLang);
                finalResult.AppendLine(result);


                await Task.Delay(600); // respeita o servidor
            }

            return finalResult.ToString();


        internal async Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang)
        {
            string translatedText = string.Empty;

            // "sl" = source language
            // "tl" = target language
            // "q" = query
            // "auto"

            string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLang}&tl={targetLang}&dt=t&q={WebUtility.UrlEncode(shortText)}";

            using HttpClient httpClient = new HttpClient();

            string response = await httpClient.GetStringAsync(url);

            // A resposta vem esquisita: [[["Olá mundo","Hello world",null,null,3]],null,"en"]
            using JsonDocument doc = JsonDocument.Parse(response);

                        
            foreach (var item in doc.RootElement[0].EnumerateArray())
            {
                translatedText += item[0].GetString();
            }

            return translatedText;
        }

        /// ////////////////////


    }
}


