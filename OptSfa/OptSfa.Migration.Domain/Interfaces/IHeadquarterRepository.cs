using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Domain.Interfaces
{
    public interface IHeadquarterRepository
    {
        public Task<List<HeadQuaterMasterViewModel>> getAllHeadquaters();
        public Task<HeadQuaterMasterViewModel> getbyId(int id);
        public Task<HeadQuaterMasterViewModel> createHeadquarter(HeadQuaterMasterViewModel headquarter);
    }
}