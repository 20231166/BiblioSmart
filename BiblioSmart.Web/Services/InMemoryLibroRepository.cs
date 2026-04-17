using BiblioSmart.Core.Entities;
using BiblioSmart.Core.Interfaces;

namespace BiblioSmart.Web.Services
{
    public class InMemoryLibroRepository : ILibroRepository
    {
        private readonly List<Libro> _libros = new()
        {
            new Libro { Id = 1, Titulo = "El Quijote", Autor = "Miguel de Cervantes", ISBN = "978-84-376-0494-7", Categoria = "Literatura", CantidadTotal = 3, CantidadDisponible = 2 },
            new Libro { Id = 2, Titulo = "Cien Años de Soledad", Autor = "Gabriel García Márquez", ISBN = "978-84-9759-083-9", Categoria = "Literatura", CantidadTotal = 2, CantidadDisponible = 2 },
            new Libro { Id = 3, Titulo = "Introducción a la Programación", Autor = "John Zelle", ISBN = "978-1-59028-005-9", Categoria = "Tecnología", CantidadTotal = 5, CantidadDisponible = 4 },
        };
        private int _nextId = 4;

        public Task<IEnumerable<Libro>> GetAllAsync() => Task.FromResult(_libros.AsEnumerable());
        public Task<Libro?> GetByIdAsync(int id) => Task.FromResult(_libros.FirstOrDefault(l => l.Id == id));
        public Task<IEnumerable<Libro>> SearchAsync(string term) =>
            Task.FromResult(_libros.Where(l =>
                l.Titulo.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                l.Autor.Contains(term, StringComparison.OrdinalIgnoreCase)).AsEnumerable());
        public Task AddAsync(Libro libro) { libro.Id = _nextId++; _libros.Add(libro); return Task.CompletedTask; }
        public Task UpdateAsync(Libro libro) {
            var i = _libros.FindIndex(l => l.Id == libro.Id);
            if (i >= 0) _libros[i] = libro;
            return Task.CompletedTask;
        }
        public Task DeleteAsync(int id) { _libros.RemoveAll(l => l.Id == id); return Task.CompletedTask; }
    }
}
