using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;

namespace xcart.Services
{
    public class ItemService : IItemService
    {
        XCartDbContext db;

        public ItemService(XCartDbContext db)
        {
            this.db = db;
        }

        #region Get All Items

        public async Task<List<Item>> GetAllItems()
        {
            if (db != null)
            {
                return await db.Item.ToListAsync();
            }
            return null;
        }
        #endregion

    }
}
