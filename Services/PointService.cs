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

        //Dependency Injection
        XCartDbContext db;

        public PointService(XCartDbContext db)
        {
            this.db = db;
        }

        #region Get Points By Employee Id
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
                              }).FirstOrDefaultAsync();                 //Returning the entry corresponding to the User Id
            }
            return null;
        }
        #endregion

        #region add Points Corresponding to User Id
        public  Point AddPoint(long points,long userId)
        {
            if (db != null)
            {
                var userPoints =  db.Point.SingleOrDefault(x => x.UserId == userId); //getting Points of an Employee

                userPoints.CurrentPoints += points;     //Updating the points of the employee
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
                Point userPoints =  db.Point.SingleOrDefault(x => x.UserId == userId); //getting Points of an Employee

                userPoints.CurrentPoints -= points;     //Updating the points of the employee
                userPoints.TotalPoints -= points;

                db.Point.Update(userPoints);
                db.SaveChanges();

                return userPoints;
            }
            return null;
        }


        #endregion

        #region Reduce Points on Checkout from cart
        public Point RemovePointsonCheckout(int points, int userid)
        {
            if (db != null)
            {
                Point userPoints = db.Point.SingleOrDefault(x => x.UserId == userid); //getting Points of an Employee

                userPoints.CurrentPoints -= points;     //Updating the points of the employee

                db.Point.Update(userPoints);
                db.SaveChanges();

                return userPoints;
            }
            return null;
        }
        #endregion


    }
}
