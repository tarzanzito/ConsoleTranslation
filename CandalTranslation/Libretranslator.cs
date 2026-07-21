using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Candal.Translation
{

    //https://libretranslate.com/languages

    public sealed class Libretranslator : ITranslator
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

        public async Task<string> TranslateAsync(string text, string sourceLang, string targetLang, CancellationToken cancellationToken = default)
        {
            if (text.Trim() == string.Empty)
                return text;

            string? result = null;

            try
            {
                await Task.Delay(FORCE_DELAY);

                var body = new
                {
                    q = text,
                    source = sourceLang,
                    target = targetLang,
                    format = "text"
                };

                string json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var httpResponse = await _httpClient.PostAsync(TRANSLATOR_URL, content, cancellationToken);

                httpResponse.EnsureSuccessStatusCode();

                string jsonResult = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                result = JsonSerializer.Deserialize<string>(jsonResult);

            }
            catch (Exception ex)
            {
                throw;
            }

            return result ?? ""; //TODO
        }

        public async Task<string> LanguagesAsync(CancellationToken cancellationToken = default)
        {
            string? result = null;

            try
            {
                var httpResponse = await _httpClient.GetAsync(TRANSLATOR_URL);

                httpResponse.EnsureSuccessStatusCode();

                string jsonResult = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                result = JsonSerializer.Deserialize<string>(jsonResult);
                result = jsonResult; //TODO:
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
            //return result?.TranslatedText ?? ""; TODO
        }

    }
}
