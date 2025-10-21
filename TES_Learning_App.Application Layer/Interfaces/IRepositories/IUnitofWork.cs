using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Application_Layer.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository AuthRepository { get; }
        
        Task<int> CompleteAsync();
    }
}
