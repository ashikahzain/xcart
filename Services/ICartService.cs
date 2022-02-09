using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface ICartService
    {
        public Task<List<EmployeeCartViewModel>> GetCartById(int id);
        public Task<int> IncreaseQuantity(int id);
        public Task<int> DecreaseQuantity(int id);
    }
}
