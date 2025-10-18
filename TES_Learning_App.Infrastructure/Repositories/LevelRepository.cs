using Microsoft.EntityFrameworkCore;
using TES_Learning_App.Domain.Entities;
using TES_Learning_App.Domain.Interfaces.Repositories;
using TES_Learning_App.Infrastructure.Data;

namespace TES_Learning_App.Infrastructure.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ApplicationDbContext _context;

        public LevelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Level>> GetAllAsync()
        {
            return await _context.Levels
                .Include(l => l.Language)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Level?> GetByIdAsync(int id)
        {
            return await _context.Levels
                .Include(l => l.Language)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Level> AddAsync(Level level)
        {
            _context.Levels.Add(level);
            await _context.SaveChangesAsync();
            return level;
        }

        public async Task UpdateAsync(Level level)
        {
            _context.Entry(level).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var level = await _context.Levels.FindAsync(id);
            if (level != null)
            {
                _context.Levels.Remove(level);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Levels.AnyAsync(e => e.Id == id);
        }
    }
}
