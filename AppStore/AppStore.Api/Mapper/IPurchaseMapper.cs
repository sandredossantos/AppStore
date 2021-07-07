using AppStore.Api.Models;
using AppStore.Domain.Entities;

namespace AppStore.Api.Mapper
{
    public interface IPurchaseMapper
    {
        Purchase ModelToEntity(PurchaseModel purchaseApplicationModel);
    }
}