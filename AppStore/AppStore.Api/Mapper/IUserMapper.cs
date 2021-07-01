using AppStore.Api.Models.JsonInput;
using AppStore.Domain.Entities;

namespace AppStore.Api.Mapper
{
    public interface IUserMapper
    {
        User ModelToEntity(UserViewModel userViewModel);
    }
}