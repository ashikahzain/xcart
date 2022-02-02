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
        public async Task<Point> GetPointsByEmployeeId(int id)
        {
            if (db != null)
            {
                //join User and Point
                return await (from point in db.Point
                              where point.User.Id == id 
                              select new Point
                              {
                                  Id = point.User.Id,
                                  CurrentPoints = point.CurrentPoints
                              }).FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion

    }
}
