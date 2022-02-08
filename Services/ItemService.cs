﻿
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
        public async Task<Item> GetItemById(int id)
        {
            var item = await db.Item.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        //Add quantity (stock update)
        public async Task<int> EditItemQuantity(ItemQuantityVm item)
        {
            var itemdetails = await db.Item.FirstOrDefaultAsync(i => i.Id == item.Id);
            var vm = new ItemQuantityVm
            {
                Id = item.Id,
                Quantity = itemdetails.Quantity + item.Quantity
            };
            vm.MaptoModel(itemdetails);
            db.SaveChanges();
            return item.Id;
        }

        //Delete Item (change is active status)

        public async Task<int> DeleteItem(int id)
        {
            var itemdetails = await db.Item.FirstOrDefaultAsync(i => i.Id == id);
            itemdetails.IsActive = false;
            db.Item.Update(itemdetails);
            await db.SaveChangesAsync();
            return id;
            
        }

        public async Task<int> UpdateItem(ItemViewModel item)
        {
            //member function to update patient
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
                db.Item.Update(itemdata);
                await db.SaveChangesAsync();
                return item.Id;
            }
            return 0;
        }
    }
}
