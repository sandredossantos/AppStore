using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(User user);
    }
}