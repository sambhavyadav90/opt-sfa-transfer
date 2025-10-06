
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface ISaveMasterService
    {
        public Task<SaveDataMaster> saveData(SaveDataMaster data);
    }
}