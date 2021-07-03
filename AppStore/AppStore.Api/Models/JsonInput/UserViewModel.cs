using System;

namespace AppStore.Api.Models.JsonInput
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string TaxNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
    }
}