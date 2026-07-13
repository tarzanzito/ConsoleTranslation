using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleTranslation
{


    internal class Libretranslator : ITranslatator
    {
        private readonly HttpClient _httpClient = new();
        private const string URL = "https://libretranslate.com/translate";
        private const int BLOCK_SIZE = 450;

        public async Task<string> TranslateAsync(string text, string sourceLang, string targetLang)
        {
            var body = new
            {
                q = text,
                source = sourceLang,
                target = targetLang,
                format = "text"
            };

            string json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync(URL, content);

            httpResponse.EnsureSuccessStatusCode();

            string jsonResult = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<string>(jsonResult);

           

            return result?.TranslatedText ?? "";
        }

        //public async Task<List<string>> TranslateSubTitlesXX(List<string> sourceList, string sourceLang, string targetLang)
        //{
        //    List<string> targetList = new();

        //    foreach (string line in sourceList)
        //    {
        //        string result = await TranslatorAsync(line, sourceLang, targetLang);
        //        targetList.Add(result);

        //        await Task.Delay(600); // respeita o servidor
        //    }

        //    return targetList;
        //}


        ////public async Task<string> TranslateSubTitles(string text, string sourceLang, string targetLang)
        ////{
        ////    var finalResult = new StringBuilder();

        ////    // 1. splt by  NEWLINE
        ////    var lineArray = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        ////    //com rest api by line
        ////    foreach (string line in lineArray)
        ////    {
        ////        // Mantem linhas vazias pra não estragar o tempo da legenda
        ////        if (string.IsNullOrWhiteSpace(line))
        ////        {
        ////            finalResult.AppendLine();
        ////            continue;
        ////        }

        ////        if (line.Length >= BLOCK_SIZE)
        ////        {
        ////            // Se a linha for muito grande, aí sim corta por tamanho
        ////            await TranslatorBigText(line, sourceLang, targetLang, finalResult);
        ////            continue;
        ////        }

        ////        string result = await TranslatorAsync(line, sourceLang, targetLang);
        ////        finalResult.AppendLine(result);

        ////        await Task.Delay(600); // respeita o servidor
        ////    }

        ////    return finalResult.ToString();
        ////}

        ////private async Task TranslatorBigText(string text, string sourceLang, string targetLang, StringBuilder finalResult)
        ////{
        ////    string[] lineArray = text.Split(new[] { ".", "!", "?" }, StringSplitOptions.None);

        ////    foreach (string line in lineArray)
        ////    {
        ////        if (line.Length < BLOCK_SIZE)
        ////        {

        ////            string result = await TranslatorAsync(line, sourceLang, targetLang);
        ////            finalResult.AppendLine(result);
        ////        }
        ////        else
        ////            throw new Exception($"Not possivel split line. Because length is gratter than {line.Length}.");
        ////    }
        ////}


        //private async Task<string> TranslatorAsync(string text, string sourceLang, string targetLang)
        //{
        //}
    }
}
