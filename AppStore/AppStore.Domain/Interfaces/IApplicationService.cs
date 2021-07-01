using AppStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IApplicationService
    {
        Task<List<Application>> GetAll();
        long BuyApp();
        Task<Application> RegisterApplication(Application application);
    }
}