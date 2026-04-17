using BiblioSmart.Core.Entities;

namespace BiblioSmart.Core.Interfaces
{
    public interface IMiembroRepository
    {
        Task<IEnumerable<Miembro>> GetAllAsync();
        Task<Miembro?> GetByIdAsync(int id);
        Task<Miembro?> GetByCedulaAsync(string cedula);
        Task AddAsync(Miembro miembro);
        Task UpdateAsync(Miembro miembro);
        Task DeleteAsync(int id);
    }
}
