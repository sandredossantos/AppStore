using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<User> GetByTaxNumber(string taxNumber);
    }
}