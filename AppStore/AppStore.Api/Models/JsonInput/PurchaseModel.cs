namespace AppStore.Api.Models.JsonInput
{
    public class PurchaseModel
    {
        public string TaxNumber { get; set; }
        public string Code { get; set; }
        public string CardNumber { get; set; }
        public string ValidThru { get; set; }
        public long SecurityCode { get; set; }
    }
}