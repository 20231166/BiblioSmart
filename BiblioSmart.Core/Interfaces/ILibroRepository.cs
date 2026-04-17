using BiblioSmart.Core.Entities;

namespace BiblioSmart.Core.Interfaces
{
    public interface ILibroRepository
    {
        Task<IEnumerable<Libro>> GetAllAsync();
        Task<Libro?> GetByIdAsync(int id);
        Task<IEnumerable<Libro>> SearchAsync(string termino);
        Task AddAsync(Libro libro);
        Task UpdateAsync(Libro libro);
        Task DeleteAsync(int id);
    }
}
