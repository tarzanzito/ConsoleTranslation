using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace Candal.Translation
{
    //NOTE-02:
    //
    //result = await Task.Run(() => {...}
    //VS
    //result = await Task.Run(ASYNC () => { await ...}
    //
    //Usa-se async na assinatura do lambda se dentro da função chamada existe await (I/O bound)
    //Quem garante a libertação da thread UI é o await.
    //
    public sealed class TranslatorHelperUserInterface : TranslatorHelper
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public TranslatorHelperUserInterface(ITranslator translator)
            : base(translator, new[] { '\0' })
        {
        }

        public TranslatorHelperUserInterface(ITranslator translator, char[] charsToSplitAtEnd)
            : base(translator, charsToSplitAtEnd)
        {
        }

        #endregion

        #region Public Methods

        public override async Task<List<string>> CreateBlockListFromStringAsync(string text, CancellationToken cancellationToken = default)
        {
            List<string> result = new();

            try
            {
                //cancellationToken.ThrowIfCancellationRequested();

                //See NOTE-02:
                result = await Task.Run(() => base.GenerateBlockListFromString(text, cancellationToken), cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public override async Task<string> CreateStringFromBlockListAsync(List<string> blockList, CancellationToken cancellationToken = default)
        {
            string result = string.Empty;

            try
            {
                //cancellationToken.ThrowIfCancellationRequested();

                //See NOTE-02:
                result = await Task.Run(() => base.GenerateStringFromBlockList(blockList, cancellationToken), cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
