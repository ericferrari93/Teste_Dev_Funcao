using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAtividadeEntrevista.CustomAttribute;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        /// <value>
        [Required]
        [Documento(ErrorMessage = "Número do CPF é inválido")]
        public string CPF { get; set; }

        public List<Beneficiario> Converter(List<BeneficiarioModel> lista)
        {
            List<Beneficiario> listaRetorno = null;

            if (lista != null && lista.Count > 0)
            {
                listaRetorno = new List<Beneficiario>();

                foreach (var modelo in lista)
                {
                    listaRetorno.Add(new Beneficiario { Nome = modelo.Nome, CPF = modelo.CPF });
                }
            }

            return listaRetorno;
        }
    }
}
