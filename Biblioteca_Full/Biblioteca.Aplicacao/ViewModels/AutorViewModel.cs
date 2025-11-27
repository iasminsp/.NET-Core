using System.ComponentModel.DataAnnotations;
using Biblioteca.Dominio.Validations;

namespace Biblioteca.Aplicacao.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do autor é obrigatório")]
        [NoDigits(ErrorMessage = "Nome do autor não pode conter números")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
    }
}