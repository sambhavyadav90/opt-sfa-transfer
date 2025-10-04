using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Services
{
    public class SaveMasterService : ISaveMasterService
    {
        private readonly ISaveDataMasterRepository saveDataMasterRepository;
        public SaveMasterService(ISaveDataMasterRepository saveDataMasterRepository)
        {
            this.saveDataMasterRepository = saveDataMasterRepository;
        }
        public Task<SaveDataMaster> saveData(SaveDataMaster data)
        {
            return saveDataMasterRepository.saveData(data);
        }
    }
}