using Biblioteca.Aplicacao.Interfaces;
using Biblioteca.Aplicacao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biblioteca.Web.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroService _service;
        private readonly Biblioteca.Dominio.Repositories.IGenericRepository<Biblioteca.Dominio.Entities.Autor> _autorRepo;

        public LivroController(ILivroService service, Biblioteca.Dominio.Repositories.IGenericRepository<Biblioteca.Dominio.Entities.Autor> autorRepo)
        {
            _service = service;
            _autorRepo = autorRepo;
        }

        public async Task<IActionResult> Index()
        {
            var livros = await _service.ObterTodos();
            return View(livros);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string q)
        {
            var livros = await _service.ObterTodos(q ?? string.Empty);
            return Json(livros);
        }

        public async Task<IActionResult> Details(int id)
        {
            var livro = await _service.ObterPorId(id);
            if (livro == null) return NotFound();
            return View(livro);
        }

        public async Task<IActionResult> Create()
        {
            var autores = await _autorRepo.GetAllAsync();
            ViewBag.Autores = new SelectList(autores, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LivroViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var autores = await _autorRepo.GetAllAsync();
                ViewBag.Autores = new SelectList(autores, "Id", "Nome");
                return View(vm);
            }

            await _service.Adicionar(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var livro = await _service.ObterPorId(id);
            if (livro == null) return NotFound();
            var autores = await _autorRepo.GetAllAsync();
            ViewBag.Autores = new SelectList(autores, "Id", "Nome", livro.AutorId);
            return View(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LivroViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var autores = await _autorRepo.GetAllAsync();
                ViewBag.Autores = new SelectList(autores, "Id", "Nome", vm.AutorId);
                return View(vm);
            }

            await _service.Atualizar(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _service.ObterPorId(id);
            if (livro == null) return NotFound();
            return View(livro);
        }

        [HttpPost, ActionName("Delete") ]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}