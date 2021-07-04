using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase> Insert(Purchase purchase);
        void UpdateStatus(Purchase purchase, string status);
        Purchase GetById(string id);
    }
}