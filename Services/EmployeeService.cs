using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;

namespace xcart.Services
{
    public class EmployeeService : IEmployeeService
    {
        XCartDbContext db;

        public EmployeeService(XCartDbContext db)
        {
            this.db = db;
        }
        public async Task<Point> MostRewarded()
        {
            if (db != null)
            { /*
                return await (from point in db.Point
                          orderby point.TotalPoints descending
                          select point.User.Id).FirstOrDefaultAsync();
                */
            }
            return null;
        }
    }
}
