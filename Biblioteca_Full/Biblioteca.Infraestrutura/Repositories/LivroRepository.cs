using Biblioteca.Dominio.Entities;
using Biblioteca.Dominio.Repositories;
using Biblioteca.Infraestrutura.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Infraestrutura.Repositories
{
    public class LivroRepository : GenericRepository<Livro>, IGenericRepository<Livro>
    {
        public LivroRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<IEnumerable<Livro>> GetByGeneroAsync(string genero) =>
            await _ctx.Livros.AsNoTracking().Where(l => l.Genero == genero).ToListAsync();
    }
}