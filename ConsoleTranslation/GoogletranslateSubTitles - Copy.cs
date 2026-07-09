//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;


//namespace ConsoleTranslation
//{
//    internal class GoogletranslateSubTitles
//    {
//        //private static string fileNameIn = String.Empty;
//        //private static string fileNameOut = String.Empty;
//        //private string sourceLang = String.Empty;
//        //private  string targetLang = String.Empty;
//        //private string result = String.Empty;

//        public async Task<string> TranslateLongTextAsync(string longText, string sourceLang, string targetLang)
//        {

//            //TODO: avançar 4500 e andar para traz até newline para não partir palavras

//            int blockSize = 4500;
//            string finalResult = string.Empty;

//            for (int i = 0; i < longText.Length; i += blockSize)
//            {
//                string block = longText.Substring(i, Math.Min(blockSize, longText.Length - i));

//                var traduzido = await TranslateAsync(block, sourceLang, targetLang);
                
//                finalResult += traduzido + "=!%!="; // " ";

//                await Task.Delay(1000); // wait  meio seg pra não levar ban
//            }

//            return finalResult; //.Trim();
//        }

//        internal async Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang)
//        {
//            string translatedText = string.Empty;

//            // "sl" = source language
//            // "tl" = target language
//            // "q" = query
//            // "auto"

//            string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLang}&tl={targetLang}&dt=t&q={WebUtility.UrlEncode(shortText)}";

//            using HttpClient httpClient = new HttpClient();

//            string response = await httpClient.GetStringAsync(url);

//            // A resposta vem esquisita: [[["Olá mundo","Hello world",null,null,3]],null,"en"]
//            using JsonDocument doc = JsonDocument.Parse(response);

                        
//            foreach (var item in doc.RootElement[0].EnumerateArray())
//            {
//                translatedText += item[0].GetString();
//            }

//            return translatedText;
//        }

//        /// ////////////////////


//    }
//}


