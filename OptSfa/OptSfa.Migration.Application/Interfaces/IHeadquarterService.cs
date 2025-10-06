using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IHeadquarterService
    {
        public Task<List<HeadQuaterMasterViewModel>> getAllHeadquaters();
        public Task<HeadQuaterMasterViewModel> getbyId(int id);
        public Task<HeadQuaterMasterViewModel> createHeadquarter(HeadQuaterMasterViewModel headquarter);
    }
}