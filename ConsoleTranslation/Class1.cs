using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTranslation
{
    internal class Class1
    {


        public static List<string> PartirTextoPorPonto(string texto, int blk)
        {
            if (string.IsNullOrEmpty(texto) || blk <= 0)
            {
                return new List<string>(); //throw new Exception
            }

            List<string> blocos = new List<string>();
            int i = 0;

            // Usamos um ciclo while porque o salto do 'i' vai variar dependendo de onde o ponto for encontrado
            while (i < texto.Length)
            {
                // 1. Define o tamanho máximo que podemos ler a partir da posição atual
                int tamanhoMaximo = Math.Min(blk, texto.Length - i);

                // 2. Tira o pedaço provisório (a nova string a analisar)
                string pedacoProvisorio = texto.Substring(i, tamanhoMaximo);

                // 3. Procura o último ponto "." de trás para a frente dentro deste pedaço
                int indicePontoNoPedaco = pedacoProvisorio.LastIndexOf('.');

                // Se encontrou um ponto E não é o último bloco completo do texto
                if (indicePontoNoPedaco != -1 && (i + tamanhoMaximo) < texto.Length)
                {
                    // O tamanho real do corte será até ao ponto (incluindo o ponto)
                    int tamanhoReal = indicePontoNoPedaco + 1;

                    // Recorta a string final com o tamanho corrigido
                    string pedacoFinal = pedacoProvisorio.Substring(0, tamanhoReal);
                    blocos.Add(pedacoFinal);

                    // O loop avança exatamente para a posição a seguir ao ponto
                    i += tamanhoReal;
                }
                else
                {
                    // Se não houver ponto (ou se for o fim do texto), aceita o pedaço inteiro
                    blocos.Add(pedacoProvisorio);
                    i += tamanhoMaximo;
                }
            }

            return blocos;
        }
    }
}
