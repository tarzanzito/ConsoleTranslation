using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleTranslation
{

    //https://libretranslate.com/languages

    internal sealed class Libretranslator : ITranslator
    {
        public const int MAX_TEXT_LENGTH_CAN_SEND = 449;
        private const string TRANSLATOR_URL = "https://libretranslate.com/translate";
        private const string LANGUAGES_URL = "https://libretranslate.com/languages";
        private const int FORCE_DELAY = 500;

        private HttpClient _httpClient = new();

        public int MaxTextLengthCanSend
        {
            get
            {
                return MAX_TEXT_LENGTH_CAN_SEND;
            }
        }

        public string TranslatorUrl
        {
            get
            {
                return TRANSLATOR_URL;
            }
        }


        public async Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang)
        {
            if (shortText.Trim() == string.Empty)
                return shortText;

            string? result = null;

            try
            {
                await Task.Delay(FORCE_DELAY);

                var body = new
                {
                    q = shortText,
                    source = sourceLang,
                    target = targetLang,
                    format = "text"
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var httpResponse = await _httpClient.PostAsync(TRANSLATOR_URL, content);

                httpResponse.EnsureSuccessStatusCode();

                string jsonResult = await httpResponse.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<string>(jsonResult);
                
            }
            catch (Exception ex)
            {

            }

            return result ?? ""; //TODO
            //return result; //?.TranslatedText ?? ""; //TODO
        }

        public async Task<string> LanguagesAsync()
        { 

            var httpResponse = await _httpClient.GetAsync(TRANSLATOR_URL);

            httpResponse.EnsureSuccessStatusCode();

            string jsonResult = await httpResponse.Content.ReadAsStringAsync();
            string? result = JsonSerializer.Deserialize<string>(jsonResult);


            return jsonResult;
            //return result?.TranslatedText ?? ""; TODO
        }

    }
}
