using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IMappedDbColumnService
    {
        public Task<List<MappedDbColumnTran>> getAll();
        public Task<List<MappedDbColumnTran>> getByDbColumn(string dbcolumnm, string dbcolumnmapping);
    }
}