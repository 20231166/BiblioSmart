
using Microsoft.AspNetCore.Mvc;
using BiblioSmart.Core.Entities;
using BiblioSmart.Core.Interfaces;

namespace BiblioSmart.Web.Controllers
{
    
    public class LibrosController : Controller
    {
        private readonly ILibroRepository _repo;
        public LibrosController(ILibroRepository repo) { _repo = repo; }

        public async Task<IActionResult> Index(string buscar)
        {
            var libros = string.IsNullOrEmpty(buscar)
                ? await _repo.GetAllAsync()
                : await _repo.SearchAsync(buscar);
            ViewBag.Buscar = buscar;
            return View(libros);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (!ModelState.IsValid) return View(libro);
            libro.CantidadDisponible = libro.CantidadTotal;
            await _repo.AddAsync(libro);
            TempData["Exito"] = "Libro registrado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var libro = await _repo.GetByIdAsync(id);
            if (libro == null) return NotFound();
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Libro libro)
        {
            if (!ModelState.IsValid) return View(libro);
            await _repo.UpdateAsync(libro);
            TempData["Exito"] = "Libro actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var libro = await _repo.GetByIdAsync(id);
            if (libro == null) return NotFound();
            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            TempData["Exito"] = "Libro eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var libro = await _repo.GetByIdAsync(id);
            if (libro == null) return NotFound();
            return View(libro);
        }
    }
}
