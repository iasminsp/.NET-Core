using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dominio.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}