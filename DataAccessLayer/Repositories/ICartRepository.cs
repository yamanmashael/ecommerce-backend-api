using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
   public  interface ICartRepository
    {

        Task<Cart> GetCartByUserIdAsync(int userId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task<int> CheckStock(int productItemId, int sizeId);
        Task<int> CartQuantityAsync(int userId);

        Task AddCartAsync(Cart cart); 
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);
        Task DeleteCartAsync(int cartId);
        Task<ProductItem> GetProductByProductItemIdAsync(int productItemId);




    }
}
