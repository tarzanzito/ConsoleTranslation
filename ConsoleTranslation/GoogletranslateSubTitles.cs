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
    internal class Googletranslator : ITranslatator
    {
        private const string URL = "https://translate.googleapis.com/translate_a/single";
        private readonly HttpClient _httpClient = new();

        public async Task<string> TranslateAsync(string text, string sourceLang, string targetLang)
        {
            string translatedText = string.Empty;

            // "sl" = source language
            // "tl" = target language
            // "q" = query
            // "auto"
            //client = gtx

            string url = $"{URL}?client=gtx&sl={sourceLang}&tl={targetLang}&dt=t&q={WebUtility.UrlEncode(text)}";

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
    }
}


