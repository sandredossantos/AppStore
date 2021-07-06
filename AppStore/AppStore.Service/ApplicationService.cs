using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using AppStore.Service.Language;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _aplicationRepository;

        public ApplicationService(IApplicationRepository aplicationRepository)
        {
            _aplicationRepository = aplicationRepository;
        }

        public async Task<List<Application>> GetAllApps()
        {
            List<Application> applications = await _aplicationRepository.GetAll();

            return applications;
        }
        public async Task RegisterApp(Application application)
        {
            if (CheckExistingApplicationByCode(application.Code).Result == true)
                throw new Exception(ServiceExceptionMsg.EXC0002);

            await _aplicationRepository.Insert(application);
        }

        private async Task<bool> CheckExistingApplicationByCode(string code)
        {
            Application application = await _aplicationRepository.GetByCode(code);

            if (application != null) return true;

            return false;
        }
    }
}