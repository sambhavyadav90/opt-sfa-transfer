using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Services
{
    public class MappedDbCoulmnService : IMappedDbColumnService
    {
        private readonly IMappedDbcolumnRepository mappedDbcolumnRepository;
        public MappedDbCoulmnService(IMappedDbcolumnRepository mappedDbcolumnRepository)
        {
            this.mappedDbcolumnRepository = mappedDbcolumnRepository;
        }
        public Task<List<MappedDbColumnTran>> getAll()
        {
            return mappedDbcolumnRepository.getAll();
        }

        public Task<List<MappedDbColumnTran>> getByDbColumn(string dbcolumnm, string dbcolumnmapping)
        {
            return mappedDbcolumnRepository.getByDbColumn(dbcolumnm, dbcolumnmapping);
        }
    }
}