using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services
{
    public class StateMasterService : IStateMasterService
    {
        private readonly IStateMasterRepository stateMasterRepository;
        public StateMasterService(IStateMasterRepository stateMasterRepository)
        {
            this.stateMasterRepository = stateMasterRepository;
        }
        public Task<List<StateMasterViewModel>> getAll(string status, string stateMain)
        {
            return stateMasterRepository.getAll(status, stateMain);
        }
    }
}