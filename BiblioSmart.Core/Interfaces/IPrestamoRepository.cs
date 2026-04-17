using BiblioSmart.Core.Entities;

namespace BiblioSmart.Core.Interfaces
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> GetAllAsync();
        Task<IEnumerable<Prestamo>> GetActivosAsync();
        Task<IEnumerable<Prestamo>> GetByMiembroAsync(int miembroId);
        Task<Prestamo?> GetByIdAsync(int id);
        Task AddAsync(Prestamo prestamo);
        Task UpdateAsync(Prestamo prestamo);
    }
}
