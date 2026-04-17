using BiblioSmart.Core.Entities;
using BiblioSmart.Core.Interfaces;

namespace BiblioSmart.Web.Services
{
    public class InMemoryMiembroRepository : IMiembroRepository
    {
        private readonly List<Miembro> _miembros = new()
        {
            new Miembro { Id = 1, NombreCompleto = "Romaldi Joel Mosquea", Cedula = "001-2023191-0", Correo = "romaldi@mail.com", Telefono = "809-555-0001", FechaRegistro = DateTime.Now },
            new Miembro { Id = 2, NombreCompleto = "María García", Cedula = "002-1234567-8", Correo = "maria@mail.com", Telefono = "809-555-0002", FechaRegistro = DateTime.Now },
        };
        private int _nextId = 3;

        public Task<IEnumerable<Miembro>> GetAllAsync() => Task.FromResult(_miembros.AsEnumerable());
        public Task<Miembro?> GetByIdAsync(int id) => Task.FromResult(_miembros.FirstOrDefault(m => m.Id == id));
        public Task<Miembro?> GetByCedulaAsync(string cedula) => Task.FromResult(_miembros.FirstOrDefault(m => m.Cedula == cedula));
        public Task AddAsync(Miembro miembro) { miembro.Id = _nextId++; _miembros.Add(miembro); return Task.CompletedTask; }
        public Task UpdateAsync(Miembro miembro) {
            var i = _miembros.FindIndex(m => m.Id == miembro.Id);
            if (i >= 0) _miembros[i] = miembro;
            return Task.CompletedTask;
        }
        public Task DeleteAsync(int id) { _miembros.RemoveAll(m => m.Id == id); return Task.CompletedTask; }
    }
}
