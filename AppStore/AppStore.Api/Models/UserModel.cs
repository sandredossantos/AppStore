using System;
using System.ComponentModel.DataAnnotations;

namespace AppStore.Api.Models
{
    public class UserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public string TaxNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Neighborhood { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}