using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services
{
    public class HeadquarterService : IHeadquarterService
    {
        private readonly IHeadquarterRepository headquarterRepository;
        public HeadquarterService(IHeadquarterRepository headquarterRepository)
        {
            this.headquarterRepository = headquarterRepository;
        }
        public Task<HeadQuaterMasterViewModel> createHeadquarter(HeadQuaterMasterViewModel headquarter)
        {
            return headquarterRepository.createHeadquarter(headquarter);
        }

        public Task<List<HeadQuaterMasterViewModel>> getAllHeadquaters()
        {
            return headquarterRepository.getAllHeadquaters();
        }

        public Task<HeadQuaterMasterViewModel> getbyId(int id)
        {
            return headquarterRepository.getbyId(id);
        }
    }
}