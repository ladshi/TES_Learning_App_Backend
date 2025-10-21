using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES_Learning_App.Application_Layer.Interfaces.IRepositories;
using TES_Learning_App.Infrastructure.Data;

namespace TES_Learning_App.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAuthRepository AuthRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            // We create instances of our repositories here, passing them the context.
            AuthRepository = new AuthRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            // This is the single place where SaveChangesAsync is called.
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
