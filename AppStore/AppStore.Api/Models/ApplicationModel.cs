using System.ComponentModel.DataAnnotations;

namespace AppStore.Api.Models
{
    public class ApplicationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public string Code { get; set; }
    }
}