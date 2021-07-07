using AppStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetAll();
        Task<Application> Insert(Application application);
        Task<Application> GetByCode(string code);
    }
}