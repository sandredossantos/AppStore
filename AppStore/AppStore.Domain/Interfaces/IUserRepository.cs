using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Insert(User user);
        Task<User> GetByTaxNumber(string taxNumber);
    }
}