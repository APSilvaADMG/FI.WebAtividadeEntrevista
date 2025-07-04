﻿using FI.AtividadeEntrevista.DML;
using System;
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
        internal long Incluir(DML.Cliente cliente)
        {
           
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("@Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@CPF", cliente.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@CEP", cliente.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Logradouro", cliente.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Telefone", cliente.Telefone));

            DataSet ds = base.Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal DML.Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        /// <summary>
        /// Valida se o cpf já existe
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificarCPFCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Pesquisa os clientes
        /// </summary>
        /// <param name="iniciarEm"></param>
        /// <param name="quantidade"></param>
        /// <param name="campoOrdenacao"></param>
        /// <param name="crescente"></param>
        /// <param name="qtd"></param>
        /// <returns></returns>
        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("@Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@CPF", cliente.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@CEP", cliente.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Logradouro", cliente.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Telefone", cliente.Telefone));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@Id", cliente.Id));

            base.Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelCliente", parametros);
        }

        private List<DML.Cliente> Converter(DataSet ds)
        {
            List<DML.Cliente> lista = new List<DML.Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Cliente cli = new DML.Cliente();
                    cli.Id = row.Field<long>("Id");
                    cli.Nome = row.Field<string>("Nome");
                    cli.Sobrenome = row.Field<string>("Sobrenome");
                    cli.CPF = row.Field<string>("CPF");
                    cli.Nacionalidade = row.Field<string>("Nacionalidade");
                    cli.CEP = row.Field<string>("CEP");
                    cli.Estado = row.Field<string>("Estado");
                    cli.Cidade = row.Field<string>("Cidade");                                      
                    cli.Logradouro = row.Field<string>("Logradouro");
                    cli.Email = row.Field<string>("Email");
                    cli.Telefone = row.Field<string>("Telefone");
                    lista.Add(cli);
                }
            }

            return lista;
        }

        public bool ValidaCpfExiste(string cpf)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("@CPF", cpf));

            DataSet ds = base.Consultar("FI_SP_VerificarCPFCliente", parametros);

            // Verifica se existe algum registro com o CPF informado
            return ds.Tables[0].Rows.Count > 0;
        }

        public bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
                return false;

            int[] multiplicadores = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            tempCpf += CalcularDigito(tempCpf, multiplicadores, 9);
            tempCpf += CalcularDigito(tempCpf, multiplicadores, 10);

            return cpf == tempCpf;
        }

        private string CalcularDigito(string cpf, int[] multiplicadores, int length)
        {
            int soma = 0;
            for (int i = 0; i < length; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicadores[i];

            int resto = soma % 11;
            return (resto < 2 ? 0 : 11 - resto).ToString();
        }


    }
}
