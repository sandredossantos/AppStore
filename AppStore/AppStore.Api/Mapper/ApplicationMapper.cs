using AppStore.Api.Models;
using AppStore.Domain.Entities;

namespace AppStore.Api.Mapper
{
    public class ApplicationMapper : IApplicationMapper
    {
        public Application ModelToEntity(ApplicationViewModel applicationViewModel)
        {
            return new Application()
            {
                Name = applicationViewModel.Name,
                Value = applicationViewModel.Value,
                Code = applicationViewModel.Code
            };
        }
    }
}