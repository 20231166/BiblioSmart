using BiblioSmart.Core.Entities;
using BiblioSmart.Core.Interfaces;

namespace BiblioSmart.Web.Services
{
    public class InMemoryPrestamoRepository : IPrestamoRepository
    {
        private readonly List<Prestamo> _prestamos = new();
        private int _nextId = 1;

        public Task<IEnumerable<Prestamo>> GetAllAsync() => Task.FromResult(_prestamos.AsEnumerable());
        public Task<Prestamo?> GetByIdAsync(int id) => Task.FromResult(_prestamos.FirstOrDefault(p => p.Id == id));
        public Task<IEnumerable<Prestamo>> GetActivosAsync() =>
            Task.FromResult(_prestamos.Where(p => p.Estado == "Activo").AsEnumerable());
        public Task<IEnumerable<Prestamo>> GetByMiembroAsync(int miembroId) =>
            Task.FromResult(_prestamos.Where(p => p.MiembroId == miembroId).AsEnumerable());
        public Task AddAsync(Prestamo prestamo) { prestamo.Id = _nextId++; _prestamos.Add(prestamo); return Task.CompletedTask; }
        public Task UpdateAsync(Prestamo prestamo) {
            var i = _prestamos.FindIndex(p => p.Id == prestamo.Id);
            if (i >= 0) _prestamos[i] = prestamo;
            return Task.CompletedTask;
        }
    }
}
