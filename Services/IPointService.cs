using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface IPointService
    {
        Task<Point> GetPointsByEmployeeId(long id);

        Point AddPoint(long points,long userid);

        Point RemovePoints(int points, long userid);

        Point RemovePointsonCheckout(int points, long userid);

        Task<int> GetPointLimit();

        Task<int> UpdatePointLimit(PointLimit pointLimit);

    }
}
