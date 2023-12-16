using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.BLL.Excecoes;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;

                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                try
                {
                    model.Id = bo.Incluir(new Cliente()
                    {
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = model.CPF
                    });

                    return Json("Cadastro efetuado com sucesso");
                }
                catch (NegocioException ex)
                {
                    Response.StatusCode = 400;

                    return Json(string.Join(Environment.NewLine, new List<string> { ex.Message }));
                }
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                try
                {
                    var modelo = new BeneficiarioModel();

                    bo.Alterar(new Cliente()
                    {
                        Id = model.Id,
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = model.CPF,
                        ListaBeneficiario = modelo.Converter(model.ListaBeneficiario)
                    });

                    return Json("Cadastro alterado com sucesso");
                }
                catch (NegocioException ex)
                {
                    Response.StatusCode = 400;

                    return Json(string.Join(Environment.NewLine, new List<string> { ex.Message }));
                }
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult NovoBeneficiario()
        {
            return PartialView("_Beneficiario");
        }

        public ActionResult IncluirBeneficiario(BeneficiarioModel modelo)
        {
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();


            }

            BoCliente bo = new BoCliente();

            if (bo.VerificarExistencia(modelo.CPF))
            {
                Response.StatusCode = 400;

                return Json(string.Join(Environment.NewLine, "Já existe um registro cadastrado com esse CPF"));
            }

            return PartialView("_ListaBeneficiado", modelo);
        }
    }
}
