using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Beneficiario cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome),
                new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF),
                new System.Data.SqlClient.SqlParameter("IdCliente", cliente.ClienteId),
            };

            DataSet ds = Consultar("FI_SP_IncBenef", parametros);

            long ret = 0;

            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }
    }
}
