
using Microsoft.AspNetCore.Mvc;
using BiblioSmart.Core.Entities;
using BiblioSmart.Core.Interfaces;

namespace BiblioSmart.Web.Controllers
{
    
    public class MiembrosController : Controller
    {
        private readonly IMiembroRepository _repo;
        public MiembrosController(IMiembroRepository repo) { _repo = repo; }

        public async Task<IActionResult> Index(string buscar)
        {
            var miembros = await _repo.GetAllAsync();
            if (!string.IsNullOrEmpty(buscar))
                miembros = miembros.Where(m =>
                    m.NombreCompleto.Contains(buscar, StringComparison.OrdinalIgnoreCase) ||
                    m.Cedula.Contains(buscar));
            ViewBag.Buscar = buscar;
            return View(miembros);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Miembro miembro)
        {
            if (!ModelState.IsValid) return View(miembro);
            await _repo.AddAsync(miembro);
            TempData["Exito"] = "Miembro registrado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var miembro = await _repo.GetByIdAsync(id);
            if (miembro == null) return NotFound();
            return View(miembro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Miembro miembro)
        {
            if (!ModelState.IsValid) return View(miembro);
            await _repo.UpdateAsync(miembro);
            TempData["Exito"] = "Miembro actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var miembro = await _repo.GetByIdAsync(id);
            if (miembro == null) return NotFound();
            var prestamos = await _repo.GetByIdAsync(id);
            return View(miembro);
        }
    }
}
