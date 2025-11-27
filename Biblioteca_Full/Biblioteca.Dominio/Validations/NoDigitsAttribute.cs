using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Biblioteca.Dominio.Validations
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = false)]
    public class NoDigitsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            var s = value.ToString();
            return !s.Any(char.IsDigit);
        }

        public override string FormatErrorMessage(string name) => $"{name} não pode conter dígitos.";
    }
}