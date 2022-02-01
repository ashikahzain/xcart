using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;

namespace xcart.Services
{
    public interface IEmployeeService
    {
        Task<Point> MostRewarded();
    }
}
