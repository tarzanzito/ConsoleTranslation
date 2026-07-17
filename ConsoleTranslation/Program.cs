using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleTranslation
{
    internal static class Program
    {
        internal static int Main(string[] args)
        {
            // Underneath the cover
            // Call asynchronous Main function
            // and waits for the result safely.
            return MainAsync(args).GetAwaiter().GetResult();
        }

        //renamed Main to MainAsync
        internal static async Task<int> MainAsync(string[] args)
        {
            //falta o CancellationToken cancellationToken  !!!!!!!!!

            Console.WriteLine("Google Translate (Version: 1.1.5");

            int ret = 0;
            //CancellationTokenSource cancellationTokenSource = new();

            try
            {

                Console.WriteLine("Validate args.");
                TranslationData translationData = ValidateArguments(args);

                Console.WriteLine($"Read all file: '{translationData.FileNameIn}'.");
                string text = await File.ReadAllTextAsync(translationData.FileNameIn, Encoding.UTF8, cancellationTokenSource);

                //Cloose translator
                //Libretranslator translator = new();
                Googletranslator translator = new();

                //Create TranslatorHelper 
                //using NewLine to split
                TranslatorHelper translatorHelper = new(translator);

                //using special chars to split
                //char[] _charsToSplitAtEnd = new[] { '.', '?' , '!'};
                //TranslatorHelper translatorHelper = new(translator, _charsToSplitAtEnd);

                //splits All file string into List<string>
                Console.WriteLine("Splits all string into List<string>.");
                List<string> textBlockList = await translatorHelper.GenerateBlockListFromStringAsync(text);

                //translate List<atring>
                Console.WriteLine("Translate List<atring>.");
                //List<string> translatedTextBlockList = await translatorHelper.TranslateBlockListAsync(textBlockList, translationData.SourceLang, translationData.TargetLang);

                //for only compare in and out files. must be equals
                List<string> translatedTextBlockList = textBlockList;

                //join List<string> into one string
                Console.WriteLine("Koin translated List<string> into one string.");
                string result = await translatorHelper.GenerateStringFromBlockListAsync(translatedTextBlockList);

                //Write translated file
                Console.WriteLine($"Write all string into file: '{translationData.FileNameOut}'.");

                await File.WriteAllTextAsync(translationData.FileNameOut, result, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error found: {ex.Message}");
                ret= 1;
            }

            Console.WriteLine($"Process terminated. Return value:{ret}.");

            return ret;
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
