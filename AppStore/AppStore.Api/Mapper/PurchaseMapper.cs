using AppStore.Api.Models.JsonInput;
using AppStore.Domain.Entities;

namespace AppStore.Api.Mapper
{
    public class PurchaseMapper : IPurchaseMapper
    {
        public Purchase ModelToEntity(PurchaseModel purchaseApplicationModel)
        {
            Purchase purchaseApplication = new Purchase()
            {
                TaxNumber = purchaseApplicationModel.TaxNumber,
                Code = purchaseApplicationModel.Code,
                CardNumber = purchaseApplicationModel.CardNumber,
                ValidThru = purchaseApplicationModel.ValidThru,
                SecurityCode = purchaseApplicationModel.SecurityCode
            };

            return purchaseApplication;
        }
    }
}