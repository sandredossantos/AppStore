using System.ComponentModel.DataAnnotations;

namespace AppStore.Api.Models
{
    public class PurchaseModel
    {
        [Required]
        public string TaxNumber { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string ValidThru { get; set; }
        
        [Required]
        public long SecurityCode { get; set; }
    }
}