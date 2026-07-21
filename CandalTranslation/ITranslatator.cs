using System.Threading;
using System.Threading.Tasks;

namespace Candal.Translation
{
    public interface ITranslator
    {
        int MaxTextLengthCanSend { get; }
        string TranslatorUrl { get; }
        Task<string> TranslateAsync(string shortText, string sourceLang, string targetLang, CancellationToken cancellationToken);
    }
}
