using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTranslation
{
    internal sealed class TranslationData
    {
        public string FileNameIn { get; init; } = String.Empty;
        public string FileNameOut { get; init; } = String.Empty;
        public string SourceLang { get; init; } = String.Empty;
        public string TargetLang { get; init; } = String.Empty;
    }
}
