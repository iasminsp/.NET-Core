using Biblioteca.Aplicacao.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Aplicacao.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroViewModel>> ObterTodos(string filtro = "");
        Task<LivroViewModel?> ObterPorId(int id);
        Task Adicionar(LivroViewModel vm);
        Task Atualizar(LivroViewModel vm);
        Task Excluir(int id);
    }
}