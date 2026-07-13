using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTranslation
{
    internal interface ITranslatator
    {
        public Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang);
    }
}
