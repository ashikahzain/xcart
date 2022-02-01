using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public class PointService : IPointService
    {
        XCartDbContext db;

        public PointService(XCartDbContext db)
        {
            this.db = db;
        }

        #region Get Points By Element Id
        public async Task<PointViewModel> GetPointsByEmployeeId(int id)
        {
            if (db != null)
            {
                //join User and Point
                return await (from user in db.User
                              from point in db.Point
                              where user.Id == id && user.Id == point.User.Id
                              select new PointViewModel
                              {
                                  Id = user.Id,
                                  CurentPoints = point.CurrentPoints
                              }).FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion

    }
}
