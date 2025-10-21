using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES_Learning_App.Application_Layer.Interfaces.IRepositories;
using TES_Learning_App.Infrastructure.Data;

namespace TES_Learning_App.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            // We don't call SaveChangesAsync here. That's the job of the Unit of Work.
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            // No SaveChangesAsync here.
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            // No SaveChangesAsync here.
            return Task.CompletedTask;
        }
    }
}
