using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace Candal.Translation
{
    //NOTE-01:
    //Api Rest - cada request gera uma nova thread.
    //  Async serve para arquitectura gerir a pool de threads
    //  Assim devemos em methods só com (cpu bound) user sincrono.
    //  await Task.Run() lança uma nova thread que degrada a pool de thread
    //  e como não tem UI não precisa libertar...

    //Outras abordagem para async  (cpu bound) seria
    // Task.CompletedTask();
    // Task.Yield() dentro de loop. mas degrada bastante a performance. Desnecessario
    //

    //CPU bound: (evitar awaits)
    //  A velocidade do processo é limitada pelo poder de processamento.
    //  A solução para torná - lo mais rápido é usar um processador
    //  com clock maior, mais núcleos, ou otimizar os algoritmos matemáticos.

    //I/O bound: (usar awaits)
    //  O processo fica a maior parte do tempo aguardando respostas externas,
    //  como a leitura de um disco (SSD/HDD) ou o tráfego em uma rede.
    //
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

                //See NOTE-01

                //Chamada Sincrona
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

                //See NOTE-01

                //Chamada Sincrona
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
