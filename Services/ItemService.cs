
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

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
                var list = await db.Item.ToListAsync();              
                return list;
            }
            return null;
        }
        #endregion

    }
}
