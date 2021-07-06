using AppStore.Api.Models;
using AppStore.Domain.Entities;

namespace AppStore.Api.Mapper
{
    public interface IUserMapper
    {
        User ModelToEntity(UserModel userViewModel);
    }
}