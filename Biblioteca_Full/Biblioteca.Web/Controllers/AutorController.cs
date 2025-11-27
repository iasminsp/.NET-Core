using Biblioteca.Aplicacao.ViewModels;
using Biblioteca.Dominio.Entities;
using Biblioteca.Dominio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Web.Controllers
{
    public class AutorController : Controller
    {
        private readonly IGenericRepository<Autor> _repo;

        public AutorController(IGenericRepository<Autor> repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index() => View(await _repo.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AutorViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var autor = new Autor { Nome = vm.Nome };
            await _repo.AddAsync(autor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var autor = await _repo.GetByIdAsync(id);
            if (autor == null) return NotFound();
            var vm = new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AutorViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var autor = new Autor { Id = vm.Id, Nome = vm.Nome };
            await _repo.UpdateAsync(autor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _repo.GetByIdAsync(id);
            if (autor == null) return NotFound();
            var vm = new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
            return View(vm);
        }

        [HttpPost, ActionName("Delete") ]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}