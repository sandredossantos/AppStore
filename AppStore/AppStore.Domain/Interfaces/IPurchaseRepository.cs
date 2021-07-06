using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IPurchaseRepository
    {
        Task Insert(Purchase purchase);
        void UpdateStatus(Purchase purchase, string status);
        Purchase GetById(string id);
        Task<Purchase> GetByCodeAndTaxNumber(string code, string taxNumber);
    }
}