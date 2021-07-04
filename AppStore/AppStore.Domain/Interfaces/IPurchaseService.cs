using AppStore.Domain.Entities;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IPurchaseService
    {
        Task CreatePurchaseOrder(Purchase purchase);
    }
}