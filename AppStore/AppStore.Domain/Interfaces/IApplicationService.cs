﻿using AppStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IApplicationService
    {
        Task<List<Application>> GetAllApps();
        Task RegisterApp(Application application);
    }
}