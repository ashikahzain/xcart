
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

        public async Task<List<Item>> GetAllActiveItems()
        {
            if (db != null)
            {
                //retrieve data from Item Model where isactive is true
                var item = await db.Item.Where(i => i.IsActive == true).ToListAsync();
                return item;
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
                //assign data to a variable
                var itemdata = new Item
                {
                    Image = image,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Points = item.Points,
                    IsActive = item.IsActive
                };
                //add to database
                await db.Item.AddAsync(itemdata);
                await db.SaveChangesAsync();
                return item.Id;


            }
            return 0 ;
        }
        #endregion

        #region get Item by ID
        public async Task<Item> GetItemById(int id)
        {
            //get item from item table using primary key
            var item = await db.Item.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        #endregion

        #region Change active status of item 

        public async Task<int> DeleteItem(int id)
        {
            //retrieve item corresponding to the given idea
            var itemdetails = await db.Item.FirstOrDefaultAsync(i => i.Id == id);
            itemdetails.IsActive = false;
            db.Item.Update(itemdetails);
            await db.SaveChangesAsync();
            return id;
            
        }
        #endregion

        #region Update Item 
        public async Task<int> UpdateItem(ItemViewModel item)
        {
            //member function to update item
            if (db != null)
            {
                //Convert Base64 Encoded string to Byte Array.
                byte[] image = Convert.FromBase64String(item.Image);
                var itemdata = new Item
                {
                    Id = item.Id,
                    Image = image,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Points = item.Points,
                    IsActive = item.IsActive
                };
                //update in database
                db.Item.Update(itemdata);
                await db.SaveChangesAsync();
                return item.Id;
            }
            return 0;
        }
        #endregion

        #region Get all inactive items
        public async Task<List<Item>> GetAllInactiveItems()
        {

            if (db != null)
            {
                //retrieve items from db where isactive value is false
                var item = await db.Item.Where(i => i.IsActive == false).ToListAsync();
                return item;
            }
            return null;
        }
        #endregion

        #region Decrease Quantity of an item
        public Item DescreaseQuantity(int itemid,int quantity)
        {
            if (db != null)
            {
                //find item in database
                Item item = db.Item.SingleOrDefault(x => x.Id == itemid);
                //change quantity
                item.Quantity -= quantity;
                db.Item.Update(item);
                db.SaveChanges();

                return item;
            }
            return null;
        }
        #endregion
    }
}
