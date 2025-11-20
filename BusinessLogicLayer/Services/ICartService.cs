using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  interface ICartService
    {



        Task<bool> MigrateCart(CartMigrationRequest cartMigration,int userId);
        Task<CartDto> GetCartItemsAsync(int userId);
        Task<CartDto> GetGuestCart(List<LocalCartItem> localCart);
        Task<bool> AddItemToCartAsync(int userId, AddToCartDto AddToCartDto);
        Task<bool> UpdateCartItemQuantityAsync(UpdateCartItemQuantityDto quantityDto);
        Task ClearCartItemAsync(int cartId);
        Task DeleteCartItemAsync(int carItemtId);
        Task<int> CartQuantityAsync(int userId);
        Task<int> CheckStockAsync(CheckStoc checkStoc);
    }
}
