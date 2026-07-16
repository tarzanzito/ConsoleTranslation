using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace ConsoleTranslation
{
    internal sealed class Googletranslator : ITranslator
    {
        public const int MAX_TEXT_LENGTH_CAN_SEND = 4999;
        private const string TRANSLATOR_URL = "https://translate.googleapis.com/translate_a/single";
        private const int FORCE_DELAY = 500;

        private HttpClient _httpClient = new();

        public int MaxTextLengthCanSend
        { 
            get
            {
                return MAX_TEXT_LENGTH_CAN_SEND;
            }
         }

        public async Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang)
        {
            int x = shortText.Length;
            if (shortText.Trim() == string.Empty)
                return shortText;

            // "sl" = source language
            // "tl" = target language
            // "q" = query
            // "auto"
            //client=gtx
            //dt=t
            //q=text

            string translatedText = string.Empty;

            try
            {
                await Task.Delay(FORCE_DELAY);

                string queryString = $"client=gtx&sl={sourceLang}&tl={targetLang}&dt=t&q={WebUtility.UrlEncode(shortText)}";
                string url = $"{TRANSLATOR_URL}?{queryString}";

                string httpResponse = await _httpClient.GetStringAsync(url);

                //httpResponse.EnsureSuccessStatusCode();

                // A resposta vem esquisita: [[["Olá mundo","Hello world",null,null,3]],null,"en"]
                using JsonDocument doc = JsonDocument.Parse(httpResponse);

                foreach (var item in doc.RootElement[0].EnumerateArray())
                {
                    translatedText += item[0].GetString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
            }

            return translatedText;
        }

    }
}
