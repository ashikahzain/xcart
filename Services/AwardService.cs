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

        #region Get All Items

        public async Task<List<Item>> GetAllAwards()
        {
            if (db != null)
            {
                var list = await db.Item.ToListAsync();
                return list;
            }
            return null;
        }
        #endregion

    }
}
