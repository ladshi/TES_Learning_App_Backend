using TES_Learning_App.Domain.Entities;

namespace TES_Learning_App.Domain.Interfaces.Repositories
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Level>> GetAllAsync();
        Task<Level?> GetByIdAsync(int id);
        Task<Level> AddAsync(Level level);
        Task UpdateAsync(Level level);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
