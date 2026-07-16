using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleTranslation
{
    internal static class Program
    {
        internal static int Main(string[] args)
        {
            // Underneath the cover
            // Call your asynchronous Main function
            // and waits for the result safely.
            return MainAsync(args).GetAwaiter().GetResult();
        }

        //renamed Main to MainAsync
        internal static async Task<int> MainAsync(string[] args)
        {
            //"Files\Amistad-1997-eng.srt" eng pt 
            //falta o CancellationToken cancellationToken  !!!!!!!!!

            Console.WriteLine("Google Translate (Version: 1.1.1");

            try
            {
                TranslationData translationData = ValidateArguments(args);

                string text = await File.ReadAllTextAsync(translationData.FileNameIn, Encoding.UTF8);
                

                Googletranslator translator = new();

                TranslatorHelper translatorHelper = new(translator);
                //char[] _charsToSplitAtEnd = new[] { '.' };
                //TranslatorHelper translatorHelper = new(translator, _charsToSplitAtEnd);


                List<string> textBlockList = await translatorHelper.GenerateBlockListFromStringAsync(text);

                List<string> translatedTextBlockList = await translatorHelper.TranslateBlockListAsync(textBlockList, translationData.SourceLang, translationData.TargetLang);

                string result = await translatorHelper.GenerateStringFromBlockListAsync(translatedTextBlockList);

                await File.WriteAllTextAsync(translationData.FileNameOut, result, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }

            Console.WriteLine($"File translated.");

            return 0;
        }

        private static TranslationData ValidateArguments(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: app inputFile sourceLang targerLang");

                if (args.Length > 0)
                    Console.WriteLine($"arg[0] ) {args[0]}");
                if (args.Length > 1)
                    Console.WriteLine($"arg[1] ) {args[1]}");
                if (args.Length > 2)
                    Console.WriteLine($"arg[2] ) {args[2]}");

                throw new Exception("Invalid app argumments.");
            }

            string fileIn = args[0];
            string source = args[1];
            string target = args[2];

            if (!File.Exists(fileIn))
                throw new Exception("Invalid File name.");

            int pos = fileIn.LastIndexOf(".");
            if (pos == -1)
                throw new Exception("Input File without extension.");

            string fileOut = fileIn.Replace($"-{source}", $"-{target}");
            //string fileOut = fileIn.Insert(pos, $"-{target}");

            TranslationData translationData = new()
            {
                FileNameIn = fileIn,
                SourceLang = source,
                TargetLang = target,
                FileNameOut = fileOut
            };

            return translationData;
        }
    }
}
