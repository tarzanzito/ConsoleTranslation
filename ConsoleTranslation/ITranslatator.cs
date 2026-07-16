using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleTranslation
{
    internal interface ITranslator
    {
        int MaxTextLengthCanSend { get; }

        Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang);
        //Task<List<string>> TranslateTextListAsync(List<string> shortTextList, string sourceLang, string targetLang);
    }


    //internal abstract class TranslatatorX
    //{
    //    protected int _maxTextLength;

    //     abstract Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang);
        
    //    protected async Task<string> TranslateTextAsync(string shortText, string sourceLang, string targetLang, bool validateLangs = true)
    //    {
    //        Validate(shortText, sourceLang, targetLang, validateLangs);
    //        return await TranslateAsync(shortText, sourceLang, targetLang);
    //    }

    //    protected async Task<List<string>> TranslateTextListAsync(List<string> shortTextList, string sourceLang, string targetLang)
    //    {
    //        ValidateList(shortTextList);

    //        bool validateLangs = true;
    //        List<string> translatedList = new();

    //        foreach (string item in shortTextList)
    //        {
    //            Validate(item, sourceLang, targetLang, validateLangs);

    //            string translatedText = await TranslateAsync(item, sourceLang, targetLang);
    //            translatedList.Add(translatedText);
    //            validateLangs = false;
    //        }

    //        return translatedList;
    //    }
        
    //    private void Validate(string shortText, string sourceLang, string targetLang, bool validateLangs)
    //    {

    //        ValidateShortText(shortText);

    //        if (validateLangs)
    //            ValidateLanguages(sourceLang, targetLang);
    //    }

    //    private void ValidateList(List<string> shortTextList)
    //    {
    //        if (shortTextList == null)
    //            throw new Exception("shortTextList is null.");
    //    }

    //    private void ValidateShortText(string shortText)
    //    {
    //        if (shortText == null)
    //            throw new Exception("shortText is null.");

    //        //if (shortText.Length < 0)
    //        //    throw new Exception("shortText length must by greatter than zero.");

    //        if (shortText.Length > _maxTextLength)
    //            throw new Exception($"shortText length is {shortText.Length} but cannot be longer than [{_maxTextLength}.");
    //    }

    //    private void ValidateLanguages(string sourceLang, string targtLang)
    //    {
    //        if (string.IsNullOrEmpty(sourceLang))
    //            throw new Exception("sourceLang is null or empty.");

    //        if (string.IsNullOrEmpty(targtLang))
    //            throw new Exception("targtLang is null or empty.");

    //        //if (charsToFind is null || charsToFind.Length == 0)
    //        //    throw new Exception("char[] charsToFind is empty.");
    //    }
    //}
    

}
