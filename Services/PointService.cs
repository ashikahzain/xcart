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
                              where point.UserId == id 
                              select new Point
                              {
                                  Id = point.UserId,
                                  CurrentPoints = point.CurrentPoints
                              }).FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion

        #region addPoints
        public  Point AddPoint(long points,long userId)
        {
            if (db != null)
            {
                var userPoints =  db.Point.SingleOrDefault(x => x.UserId == userId);

                userPoints.CurrentPoints += points;
                userPoints.TotalPoints += points;


                db.Point.Update(userPoints);
                 db.SaveChanges();

                return userPoints;
            }
            return null;
        }
        #endregion

        #region Remove Points
        public Point RemovePoints(int points, int userId)
        {
            if (db != null)
            {
                Point userPoints =  db.Point.SingleOrDefault(x => x.UserId == userId);
                userPoints.CurrentPoints -= points;
                userPoints.TotalPoints -= points;

                db.Point.Update(userPoints);
                 db.SaveChanges();

                return userPoints;
            }
            return null;
        }


        #endregion

        public Point RemovePointsonCheckout(int points, int userid)
        {
            if (db != null)
            {
                Point userPoints = db.Point.SingleOrDefault(x => x.UserId == userid);
                userPoints.CurrentPoints -= points;
                db.Point.Update(userPoints);
                db.SaveChanges();

                return userPoints;
            }
            return null;
        }

    }
}
