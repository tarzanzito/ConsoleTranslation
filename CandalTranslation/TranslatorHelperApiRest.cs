using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace Candal.Translation
{
    public sealed class TranslatorHelperApiRest : TranslatorHelper
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public TranslatorHelperApiRest(ITranslator translator)
            : base(translator, new[] { '\0' })
        {
        }

        public TranslatorHelperApiRest(ITranslator translator, char[] charsToSplitAtEnd)
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

                result = base.GenerateBlockListFromString(text, cancellationToken);
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
                result = base.GenerateStringFromBlockList(blockList, cancellationToken);
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
