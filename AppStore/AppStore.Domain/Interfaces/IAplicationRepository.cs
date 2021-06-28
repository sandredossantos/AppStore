﻿using AppStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Domain.Interfaces
{
    public interface IAplicationRepository
    {
        Task<List<Application>> GetAllApps();
        Task<Application> RegisterApplication(Application application);
    }
}