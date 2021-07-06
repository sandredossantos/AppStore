using AppStore.Api.Models;
using AppStore.Domain.Entities;

namespace AppStore.Api.Mapper
{
    public interface IApplicationMapper
    {
        Application ModelToEntity(ApplicationViewModel applicationViewModel);
    }
}