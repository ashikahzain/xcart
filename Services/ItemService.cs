
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        #region Add Item
        public async Task<int> AddItem(ItemViewModel item)
        {
            if (db != null)
            {

                //Convert Base64 Encoded string to Byte Array.
                byte[] image = Convert.FromBase64String(item.Image);      
                var itemdata = new Item();
                itemdata.Image = image;
                itemdata.Name = item.Name;
                itemdata.Quantity = item.Quantity;
                itemdata.Points = item.Points;
                itemdata.IsActive = item.IsActive;
                await db.Item.AddAsync(itemdata);
                await db.SaveChangesAsync();
                return item.Id;


            }
            return 0 ;
        }

        #endregion

    }
}
