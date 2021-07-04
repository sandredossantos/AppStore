using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase> Insert(Purchase purchase);        
    }
}