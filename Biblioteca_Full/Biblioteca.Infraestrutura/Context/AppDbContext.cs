using Biblioteca.Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infraestrutura.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var l = modelBuilder.Entity<Livro>();
            l.HasKey(x => x.Id);
            l.Property(x => x.Titulo).IsRequired().HasMaxLength(150);
            l.Property(x => x.Genero).HasMaxLength(60);
            l.Property(x => x.AnoPublicacao).IsRequired();
            l.Property(x => x.AutorId).IsRequired();

            var a = modelBuilder.Entity<Autor>();
            a.HasKey(x => x.Id);
            a.Property(x => x.Nome).IsRequired().HasMaxLength(100);

            l.HasOne(x => x.Autor)
             .WithMany(a => a.Livros)
             .HasForeignKey(x => x.AutorId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}