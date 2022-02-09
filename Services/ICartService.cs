using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xcart.Models;
using xcart.ViewModel;

namespace xcart.Services
{
    public interface ICartService
    {
        public Task<List<EmployeeCartViewModel>> GetCartById(int id);


        public Task<List<Cart>> GetAllCartById(int id);

        Task<int> AddToCart(Cart cart);

        Task<Cart> DeleteCart(int id);
    }
}
