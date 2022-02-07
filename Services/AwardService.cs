using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;

namespace xcart.Services
{
    public class AwardService : IAwardService
    {
        XCartDbContext db;
        public AwardService(XCartDbContext db)
        {
            this.db = db;
        }

        #region Get All Awards
        public async Task<List<Award>> GetAllAwards()
        {
            if (db != null)
            {
                var list = await db.Award.ToListAsync();
                return list;
            }
            return null;
        }
        #endregion

        #region Add new Award
        public async Task<int> AddAward(Award award)
        {
            if(db != null)
            {
                await db.Award.AddAsync(award);
                await db.SaveChangesAsync();
                return award.Id;
            }
            return 0;
        }
        #endregion

        #region Update Award
        public async Task UpdateAward(Award award)
        {
            if(db != null)
            {
                db.Award.Update(award);
                await db.SaveChangesAsync();
            }
        }
        #endregion

        #region Update Award
        public async Task DeleteAward(int id)
        {
            Award award = db.Award.FirstOrDefault(aid => aid.Id == id);
            if (award != null)
            {
                award.IsActive = false;
                await db.SaveChangesAsync();
            }
        }
        #endregion
    }
}
