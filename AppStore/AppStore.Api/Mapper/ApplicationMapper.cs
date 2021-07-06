using AppStore.Api.Models;
using AppStore.Domain.Entities;

namespace AppStore.Api.Mapper
{
    public class ApplicationMapper : IApplicationMapper
    {
        public Application ModelToEntity(ApplicationModel applicationViewModel)
        {
            Application application = new Application()
            {
                Name = applicationViewModel.Name,
                Value = applicationViewModel.Value,
                Code = applicationViewModel.Code
            };

            return application;
        }
    }
}