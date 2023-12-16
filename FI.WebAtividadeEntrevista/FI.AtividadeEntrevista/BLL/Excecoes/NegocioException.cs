using System;

namespace FI.AtividadeEntrevista.BLL.Excecoes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Exception" />
    public class NegocioException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegocioException"/> class.
        /// </summary>
        /// <param name="mensagem">The mensagem.</param>
        public NegocioException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
