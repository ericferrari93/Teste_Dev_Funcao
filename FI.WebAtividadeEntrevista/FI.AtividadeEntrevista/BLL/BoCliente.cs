using FI.AtividadeEntrevista.BLL.Excecoes;
using FI.AtividadeEntrevista.DAL.Beneficiarios;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            if (this.VerificarExistencia(cliente.CPF))
            {
                throw new NegocioException("Já existe um registro cadastrado com esse CPF");
            }

            DAL.DaoCliente cli = new DAL.DaoCliente();
            var retrorno = cli.Incluir(cliente);

            if (cliente.ListaBeneficiario != null && cliente.ListaBeneficiario.Count > 0)
            {
                DaoBeneficiario bne = new DaoBeneficiario();

                foreach (var beneficiario in cliente.ListaBeneficiario)
                {
                    beneficiario.ClienteId = retrorno;

                    bne.Incluir(beneficiario);
                }
            }

            return retrorno;
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Alterar(cliente);

            if (cliente.ListaBeneficiario != null && cliente.ListaBeneficiario.Count > 0)
            {
                DaoBeneficiario bne = new DaoBeneficiario();

                foreach (var beneficiario in cliente.ListaBeneficiario)
                {
                    beneficiario.ClienteId = cliente.Id;

                    bne.Incluir(beneficiario);
                }
            }
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF);
        }
    }
}
