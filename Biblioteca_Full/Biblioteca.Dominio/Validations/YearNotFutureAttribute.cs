using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dominio.Validations
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = false)]
    public class YearNotFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            if (int.TryParse(value.ToString(), out int year))
            {
                return year <= DateTime.UtcNow.Year;
            }
            return false;
        }

        public override string FormatErrorMessage(string name) => $"{name} não pode ser um ano no futuro.";
    }
}