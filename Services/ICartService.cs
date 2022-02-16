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
        public Task<List<EmployeeCartViewModel>> GetCartById(long id);
        public List<Cart> GetCartByUserId(long id);
        public Task<List<Cart>> GetAllCartById(int id);
        Task<long> AddToCart(Cart cart);
        Task<Cart> DeleteCart(int id);
        public Task<int> IncreaseQuantity(int id);
        public Task<int> DecreaseQuantity(int id);
        Task<List<Cart>> DeleteCartbyUserId(long id);

    }
}
