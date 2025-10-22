using System;
using System.Threading.Tasks;
using TES_Learning_App.Application_Layer.Interfaces.IRepositories;
using TES_Learning_App.Domain.Entities;
using TES_Learning_App.Infrastructure.Data;
namespace TES_Learning_App.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAuthRepository AuthRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; } // Use the specific interface
        public IGenericRepository<Role> RoleRepository { get; private set; }
        public IGenericRepository<Admin> AdminRepository { get; private set; }
        public IGenericRepository<Language> LanguageRepository { get; private set; }
        public IGenericRepository<Level> LevelRepository { get; private set; }
        public IGenericRepository<Stage> StageRepository { get; private set; }
        public IGenericRepository<MainActivity> MainActivityRepository { get; private set; }
        public IGenericRepository<ActivityType> ActivityTypeRepository { get; private set; }
        public IGenericRepository<Activity> ActivityRepository { get; private set; }
        public IGenericRepository<StudentProgress> StudentProgressRepository { get; private set; }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}