﻿using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome),
                new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome),
                new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF),
                new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade),
                new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP),
                new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado),
                new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade),
                new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro),
                new System.Data.SqlClient.SqlParameter("Email", cliente.Email),
                new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone)
            };

            DataSet ds = Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Id", Id)
            };

            DataSet ds = Consultar("FI_SP_ConsCliente", parametros);
            List<Cliente> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = Consultar("FI_SP_VerificaCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm),
                new System.Data.SqlClient.SqlParameter("quantidade", quantidade),
                new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao),
                new System.Data.SqlClient.SqlParameter("crescente", crescente)
            };

            DataSet ds = Consultar("FI_SP_PesqCliente", parametros);
            List<Cliente> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = Consultar("FI_SP_ConsCliente", parametros);
            List<Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome),
                new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome),
                new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF),
                new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade),
                new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP),
                new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado),
                new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade),
                new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro),
                new System.Data.SqlClient.SqlParameter("Email", cliente.Email),
                new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone),
                new System.Data.SqlClient.SqlParameter("ID", cliente.Id)
            };

            Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Id", Id)
            };

            Executar("FI_SP_DelCliente", parametros);
        }

        private List<Cliente> Converter(DataSet ds)
        {
            List<Cliente> lista = new List<Cliente>();

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Cliente cli = new Cliente();
                    cli.Id = row.Field<long>("Id");
                    cli.CEP = row.Field<string>("CEP");
                    cli.Cidade = row.Field<string>("Cidade");
                    cli.Email = row.Field<string>("Email");
                    cli.Estado = row.Field<string>("Estado");
                    cli.Logradouro = row.Field<string>("Logradouro");
                    cli.Nacionalidade = row.Field<string>("Nacionalidade");
                    cli.Nome = row.Field<string>("Nome");
                    cli.Sobrenome = row.Field<string>("Sobrenome");
                    cli.Telefone = row.Field<string>("Telefone");
                    cli.CPF = row.Field<string>("CPF");
                    lista.Add(cli);
                }
            }

            return lista;
        }
    }
}