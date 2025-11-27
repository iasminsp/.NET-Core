using System.ComponentModel.DataAnnotations;
using Biblioteca.Dominio.Validations;

namespace Biblioteca.Aplicacao.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(60)]
        public string Genero { get; set; } = string.Empty;

        [YearNotFuture(ErrorMessage = "Ano não pode ser no futuro")]
        public int AnoPublicacao { get; set; }

        [Required(ErrorMessage = "Autor é obrigatório")]
        public int AutorId { get; set; }

        public string AutorNome { get; set; } = string.Empty;
    }
}