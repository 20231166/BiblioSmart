
using Microsoft.AspNetCore.Mvc;
using BiblioSmart.Core.Entities;
using BiblioSmart.Core.Interfaces;

namespace BiblioSmart.Web.Controllers
{
    
    public class PrestamosController : Controller
    {
        private readonly IPrestamoRepository _prestamoRepo;
        private readonly ILibroRepository _libroRepo;
        private readonly IMiembroRepository _miembroRepo;

        public PrestamosController(IPrestamoRepository prestamoRepo,
            ILibroRepository libroRepo, IMiembroRepository miembroRepo)
        {
            _prestamoRepo = prestamoRepo;
            _libroRepo = libroRepo;
            _miembroRepo = miembroRepo;
        }

        public async Task<IActionResult> Index()
        {
            var prestamos = await _prestamoRepo.GetAllAsync();
            return View(prestamos);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Libros = await _libroRepo.GetAllAsync();
            ViewBag.Miembros = await _miembroRepo.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prestamo prestamo)
        {
            var libro = await _libroRepo.GetByIdAsync(prestamo.LibroId);
            if (libro == null || libro.CantidadDisponible <= 0)
            {
                ModelState.AddModelError("", "El libro no está disponible para préstamo.");
                ViewBag.Libros = await _libroRepo.GetAllAsync();
                ViewBag.Miembros = await _miembroRepo.GetAllAsync();
                return View(prestamo);
            }
            prestamo.FechaPrestamo = DateTime.Now;
            prestamo.Estado = "Activo";
            libro.CantidadDisponible--;
            await _libroRepo.UpdateAsync(libro);
            await _prestamoRepo.AddAsync(prestamo);
            TempData["Exito"] = "Préstamo registrado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Devolver(int id)
        {
            var prestamo = await _prestamoRepo.GetByIdAsync(id);
            if (prestamo == null) return NotFound();
            return View(prestamo);
        }

        [HttpPost, ActionName("Devolver")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DevolverConfirmed(int id)
        {
            var prestamo = await _prestamoRepo.GetByIdAsync(id);
            if (prestamo == null) return NotFound();
            if (prestamo.Estado == "Devuelto")
            {
                TempData["Error"] = "Este préstamo ya fue devuelto.";
                return RedirectToAction(nameof(Index));
            }
            prestamo.Estado = "Devuelto";
            prestamo.FechaDevolucionReal = DateTime.Now;
            var libro = await _libroRepo.GetByIdAsync(prestamo.LibroId);
            if (libro != null)
            {
                libro.CantidadDisponible++;
                await _libroRepo.UpdateAsync(libro);
            }
            await _prestamoRepo.UpdateAsync(prestamo);
            TempData["Exito"] = "Devolución registrada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reporte()
        {
            var prestamos = await _prestamoRepo.GetAllAsync();
            var libros = await _libroRepo.GetAllAsync();
            ViewBag.TotalLibros = libros.Sum(l => l.CantidadTotal);
            ViewBag.LibrosPrestados = prestamos.Count(p => p.Estado == "Activo");
            ViewBag.LibrosDisponibles = libros.Sum(l => l.CantidadDisponible);
            return View(prestamos);
        }
    }
}
