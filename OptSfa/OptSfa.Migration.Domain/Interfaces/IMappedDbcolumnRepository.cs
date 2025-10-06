
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Domain.Interfaces
{
    public interface IMappedDbcolumnRepository
    {
        public Task<List<MappedDbColumnTran>> getAll();
        public Task<List<MappedDbColumnTran>> getByDbColumn(string dbcolumnm, string dbcolumnmapping);
    }
}