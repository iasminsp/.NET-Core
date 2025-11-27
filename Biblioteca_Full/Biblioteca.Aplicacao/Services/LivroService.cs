using Biblioteca.Aplicacao.Interfaces;
using Biblioteca.Aplicacao.ViewModels;
using Biblioteca.Dominio.Entities;
using Biblioteca.Dominio.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Aplicacao.Services
{
    public class LivroService : ILivroService
    {
        private readonly IGenericRepository<Livro> _livroRepo;
        private readonly IGenericRepository<Autor> _autorRepo;
        private readonly Biblioteca.Aplicacao.Interfaces.IMapper _mapper;

        public LivroService(IGenericRepository<Livro> livroRepo, IGenericRepository<Autor> autorRepo, Biblioteca.Aplicacao.Interfaces.IMapper mapper)
        {
            _livroRepo = livroRepo;
            _autorRepo = autorRepo;
            _mapper = mapper;
        }

        public async Task Adicionar(LivroViewModel vm)
        {
            var livro = _mapper.Map<LivroViewModel, Livro>(vm);
            await _livroRepo.AddAsync(livro);
        }

        public async Task Atualizar(LivroViewModel vm)
        {
            var livro = _mapper.Map<LivroViewModel, Livro>(vm);
            await _livroRepo.UpdateAsync(livro);
        }

        public async Task Excluir(int id) => await _livroRepo.DeleteAsync(id);

        public async Task<IEnumerable<LivroViewModel>> ObterTodos(string filtro = "")
        {
            var livros = (await _livroRepo.GetAllAsync()).ToList();
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                livros = livros.Where(l => l.Titulo.Contains(filtro, System.StringComparison.OrdinalIgnoreCase) || (l.Genero != null && l.Genero.Contains(filtro, System.StringComparison.OrdinalIgnoreCase))).ToList();
            }
            var autores = (await _autorRepo.GetAllAsync()).ToDictionary(a => a.Id, a => a.Nome);
            var vms = livros.Select(l =>
            {
                var vm = _mapper.Map<Livro, LivroViewModel>(l);
                vm.AutorNome = autores.ContainsKey(l.AutorId) ? autores[l.AutorId] : string.Empty;
                return vm;
            });
            return vms;
        }

        public async Task<LivroViewModel?> ObterPorId(int id)
        {
            var l = await _livroRepo.GetByIdAsync(id);
            if (l == null) return null;
            var autor = await _autorRepo.GetByIdAsync(l.AutorId);
            var vm = _mapper.Map<Livro, LivroViewModel>(l);
            vm.AutorNome = autor?.Nome ?? string.Empty;
            return vm;
        }
    }
}