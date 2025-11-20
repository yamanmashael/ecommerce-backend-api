using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.cartItems)
                    .ThenInclude(ci => ci.ProductItem)
                        .ThenInclude(pi => pi.Product)
                            .ThenInclude(b => b.Brand)
                .Include(c => c.cartItems)
                    .ThenInclude(ci => ci.ProductItem)
                        .ThenInclude(pi => pi.Color)
                .Include(c => c.cartItems)
                    .ThenInclude(ci => ci.Size)
               
                .Include(c => c.cartItems)
                    .ThenInclude(ci => ci.ProductItem)
                        .ThenInclude(pi => pi.ProductImages) 
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems
                                 .Include(ci => ci.ProductItem)
                                 .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        }


        public async Task AddCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);    
            await _context.SaveChangesAsync();      
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Add (cartItem);  
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
           var cartItem= await _context.CartItems.FindAsync(cartItemId);    
            _context.CartItems.Remove(cartItem);    
            await _context.SaveChangesAsync();
        }

        public async Task<int> CheckStock(int productItemId, int sizeId)
        {
            var productItem = await _context.ProductItemSize.Where(x => x.PrductItemId == productItemId && x.SizeId == sizeId).FirstOrDefaultAsync();
            return productItem.Stock;
        }

        public async Task DeleteCartAsync(int cartId)
        {
            var cartItems = await _context.CartItems
           .Where(x => x.CartId == cartId)
           .ToListAsync();

            if (cartItems.Any())
            {
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }

        }





        public async Task<ProductItem> GetProductByProductItemIdAsync(int productItemId)
        {
            var productItem = await _context.ProductItem.Include(pi => pi.Product)
                .ThenInclude(p => p.Brand).Include(pi => pi.Color).Include(pi => pi.ProductImages)
               .Include(pi => pi.ProductItemSizes).ThenInclude(pis => pis.Size)
                .Where(pi => pi.Id == productItemId).FirstOrDefaultAsync();

            return productItem;


        }

        public async Task<int> CartQuantityAsync(int userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.cartItems)  
                .SumAsync(ci => ci.Quantity);   
        }
    }
}
