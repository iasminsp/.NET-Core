using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Biblioteca.Infraestrutura.Context;

#nullable disable

namespace Biblioteca.Infraestrutura.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Biblioteca.Dominio.Entities.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("Biblioteca.Dominio.Entities.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnoPublicacao")
                        .HasColumnType("int");

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<string>("Genero")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("Biblioteca.Dominio.Entities.Livro", b =>
                {
                    b.HasOne("Biblioteca.Dominio.Entities.Autor", "Autor")
                        .WithMany("Livros")
                        .HasForeignKey("AutorId")
                        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade)
                        .IsRequired();
                });
        }
    }
}