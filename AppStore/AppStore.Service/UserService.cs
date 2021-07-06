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

        public async Task CreateUser(User user)
        {
            if (CheckExistingUser(user.TaxNumber).Result == true)
                throw new Exception(string.Format(ServiceExceptionMsg.EXC0001, user.TaxNumber));

            await _userRepository.Insert(user);
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