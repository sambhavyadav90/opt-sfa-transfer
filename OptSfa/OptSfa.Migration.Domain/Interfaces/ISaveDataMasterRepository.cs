using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Domain.Interfaces
{
    public interface ISaveDataMasterRepository
    {
        public Task<SaveDataMaster> saveData(SaveDataMaster data);
    }
}