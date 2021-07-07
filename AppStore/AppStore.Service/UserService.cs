using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using System.Threading.Tasks;
using System;
using AppStore.Service.Language;

namespace AppStore.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(User user)
        {
            if (CheckExistingUser(user.TaxNumber).Result == true)
                throw new Exception(string.Format(ServiceExceptionMsg.EXC0001, user.TaxNumber));

            User newUser = await _userRepository.Insert(user);

            return newUser; 
        }

        public async Task<User> GetByTaxNumber(string taxNumber)
        {
            User user = await _userRepository.GetByTaxNumber(taxNumber);

            return user;
        }

        private async Task<bool> CheckExistingUser(string taxNumber)
        {
            User user = await _userRepository.GetByTaxNumber(taxNumber);

            if (user != null) return true;

            return false;
        }
    }
}