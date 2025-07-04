using FI.AtividadeEntrevista.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebAtividadeEntrevista.Models.Validations
{
    public sealed class CPFCheckExistClienteValidator : ValidationAttribute, IClientValidatable
    {
        public CPFCheckExistClienteValidator() { }

        public override bool IsValid(object value)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString()))
                return false;

            var result = CheckIfNotExistInDatabase(value.ToString());

            return result;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "CPFCheckExistClienteValidator"
            };
        }

        private static bool CheckIfNotExistInDatabase(string cpf)
        {
            var clientBll = new BoCliente();

            var result = clientBll.VerificarExistencia(cpf);

            return !result;
        }
    }
}