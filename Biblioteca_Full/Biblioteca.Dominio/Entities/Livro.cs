using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dominio.Entities
{
    public class Livro
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }

        [StringLength(60)]
        public string Genero { get; set; }

        [Range(0, 2100)]
        public int AnoPublicacao { get; set; }

        [Required]
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}