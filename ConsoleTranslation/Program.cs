using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ConsoleTranslation
{
    internal class TranslationInfo
    {

        public string FileNameIn { get; init; } = String.Empty;
        public string FileNameOut { get; init; } = String.Empty;
        public string SourceLang { get; init; } = String.Empty;
        public string TargetLang { get; init; } = String.Empty;
    }

    internal static class Program
    {
        internal static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Google Translate (Version: 1.1.1");

            try
            {
                TranslationInfo translationInfo = ValidateArguments(args);

                string text = System.IO.File.ReadAllText(translationInfo.FileNameIn, Encoding.UTF8);

                LibretranslateSubTitles translatorL = new();
                string resultL = await translatorL.TranslateSubTitles(text, translationInfo.SourceLang, translationInfo.TargetLang);

                File.WriteAllText(translationInfo.FileNameOut, resultL, Encoding.UTF8);

                GoogletranslateSubTitles translatorG = new();

                string resultG = await translatorL.TranslateSubTitles(text, translationInfo.SourceLang, translationInfo.TargetLang);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }

            Console.WriteLine($"File translated.");

            return 0;
        }

        private static TranslationInfo ValidateArguments(string[] args)
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

            string fileOut = fileIn.Insert(pos, $"-{target}");

            TranslationInfo translationInfo = new()
            {
                FileNameIn = fileIn,
                SourceLang = source,
                TargetLang = target
            };

            return translationInfo;
        }

    }
}



