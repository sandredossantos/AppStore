using AppStore.Api.Models.JsonInput;
using AppStore.Domain.Entities;
using System;

namespace AppStore.Api.Mapper
{
    public class UserMapper : IUserMapper
    {
        public User ModelToEntity(UserViewModel userViewModel)
        {
            try
            {
                Address address = new Address()
                {
                    Country = userViewModel.Country,
                    State = userViewModel.State,
                    City = userViewModel.City,
                    ZipCode = userViewModel.ZipCode,
                    Neighborhood = userViewModel.Neighborhood,
                    Street = userViewModel.Street,
                    Number = userViewModel.Number
                };

                User user = new User()
                {
                    Name = userViewModel.Name,
                    TaxNumber = userViewModel.TaxNumber,
                    BirthDate = userViewModel.BirthDate,
                    CreationDate = DateTime.Now,
                    Sex = userViewModel.Sex,
                    Address = address
                };

                return user;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}