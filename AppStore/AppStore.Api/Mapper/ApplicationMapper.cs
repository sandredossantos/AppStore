using AppStore.Api.Models.JsonInput;
using AppStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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