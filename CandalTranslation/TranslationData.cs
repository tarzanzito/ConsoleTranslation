using System;
using System.Collections.Generic;
using System.Text;

namespace Candal.Translation
{
    public sealed class TranslationData
    {
        public required string FileNameIn { get; init; }// = String.Empty;
        public required string FileNameOut { get; init; } // = String.Empty;
        public required string SourceLang { get; init; }// = String.Empty;
        public required string TargetLang { get; init; }// = String.Empty;
    }
}
