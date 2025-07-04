using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly BoBeneficiario _boBeneficiario;

        public BeneficiarioController()
        {
            _boBeneficiario = new BoBeneficiario();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        // GET: Beneficiario/Edit/5
        [HttpGet]
        public ActionResult Alterar(long id)
        {
            var _beneficiario = _boBeneficiario.Consultar(id);
            Models.BeneficiarioModel model = null;

            if (_beneficiario != null)
            {
                model = BeneficiarioModel.ConvertToModel(_beneficiario);
            }

            return View(model);
        }

        // POST: Beneficiario/Edit/5
        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
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
                var _beneficiario = model.GetBeneficiario();
                _boBeneficiario.Alterar(_beneficiario);

                return Json("Cadastro alterado com sucesso");
            }
        }

        // GET: Beneficiario/Delete/5
        public ActionResult Excluir(long id)
        {
            var _beneficiario = _boBeneficiario.Consultar(id);
            Models.BeneficiarioModel model = null;

            if (_beneficiario != null)
            {
                model = BeneficiarioModel.ConvertToModel(_beneficiario);
            }

            return View(model);
        }

        // POST: Beneficiario/Delete/5
        [HttpPost]
        public JsonResult Excluir(BeneficiarioModel model)
        {
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
                var _beneficiario = model.GetBeneficiario();
                _boBeneficiario.Excluir(_beneficiario.Id);

                return Json("Cadastro excluido com sucesso");
            }
        }
    }
}
