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
    public sealed class CPFCheckDigitValidator : ValidationAttribute, IClientValidatable
    {
        public CPFCheckDigitValidator() { }

        public override bool IsValid(object value)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString()))
                return false;

            var result = CheckDigitVerification(value.ToString());

            return result;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "CPFCheckDigitValidator"
            };
        }

        private static string ConvertToNumber(string cpf)
        {
            var regex = new Regex(@"[^0-9]");

            var result = regex.Replace(cpf, string.Empty).Trim();

            return result;
        }
        private static bool CheckDigitVerification(string cpf)
        {
            bool isEqual = true;
            int[] numbers = new int[11];
            int sum = 0,
                result;

            cpf = ConvertToNumber(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            for (int i = 1; i < 11 && isEqual; i++)
                if (cpf[i] != cpf[0])
                    isEqual = false;

            if (isEqual || cpf == "12345678909")
                return false;

            for (int i = 0; i < 11; i++)
                numbers[i] = int.Parse(cpf[i].ToString());

            for (int i = 0; i < 9; i++)
                sum += (10 - i) * numbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - result)
                return false;

            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += (11 - i) * numbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else
            if (numbers[10] != 11 - result)
                return false;

            return true;

        }
        
    }
}