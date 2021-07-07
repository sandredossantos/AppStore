using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase> CreatePurchaseOrder(Purchase purchase);
        void UpdateStatus(Purchase purchase, string status);
        Purchase GetById(string id);
    }
}