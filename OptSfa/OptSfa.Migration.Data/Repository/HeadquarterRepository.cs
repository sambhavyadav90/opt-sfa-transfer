using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Data.Repository
{
    public class HeadquarterRepository : IHeadquarterRepository
    {
        private readonly AppDbContext db;
        public HeadquarterRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<HeadQuaterMasterViewModel> createHeadquarter(HeadQuaterMasterViewModel headquarter)
        {
            if (headquarter == null)
                throw new ArgumentNullException(nameof(headquarter));

            HeadquarterMaster newHeadquarter = new HeadquarterMaster
            {
                districtId = headquarter.districtId,
                district = headquarter.district,
                hqGroupId = headquarter.hqGroupId,
                status = headquarter.status,
                districtCode = headquarter.districtCode,
                stateMain = headquarter.stateMain
            };

            db.headquarterMasters.Add(newHeadquarter);
            await db.SaveChangesAsync();

            headquarter.districtId = newHeadquarter.districtId; 
            return headquarter;
        }

        public async Task<List<HeadQuaterMasterViewModel>> getAllHeadquaters()
        {
            List<HeadquarterMaster> allheadquarters = await db.headquarterMasters.ToListAsync();

            List<HeadQuaterMasterViewModel> allHeadquartersVM = allheadquarters.Select(h => new HeadQuaterMasterViewModel
            {
                districtId = h.districtId,
                district = h.district,
                hqGroupId = h.hqGroupId,
                status = h.status,
                districtCode = h.districtCode,
                stateMain = h.stateMain
            }).ToList();

            return allHeadquartersVM;
        }

        public async Task<HeadQuaterMasterViewModel> getbyId(int id)
        {
            HeadquarterMaster? hq = await db.headquarterMasters.FirstOrDefaultAsync(h => h.districtId == id);

            if (hq == null) return null;

            return new HeadQuaterMasterViewModel
            {
                districtId = hq.districtId,
                district = hq.district,
                hqGroupId = hq.hqGroupId,
                status = hq.status,
                districtCode = hq.districtCode,
                stateMain = hq.stateMain
            };
        }
    }
}
