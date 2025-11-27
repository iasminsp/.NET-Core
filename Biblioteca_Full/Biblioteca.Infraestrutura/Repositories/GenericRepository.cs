using Biblioteca.Dominio.Repositories;
using Biblioteca.Infraestrutura.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Infraestrutura.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _ctx;

        public GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(T entity)
        {
            _ctx.Set<T>().Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _ctx.Set<T>().Remove(entity);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _ctx.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _ctx.Set<T>().FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _ctx.Set<T>().Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}