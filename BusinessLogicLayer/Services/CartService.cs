using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly ISizeRepository _sizeRepository;


        public CartService(ICartRepository repository, ISizeRepository sizeRepository)
        {
            _repository = repository;
            _sizeRepository = sizeRepository;       
        }







        public async Task<CartDto> GetCartItemsAsync(int userId)
        {
            var cart = await _repository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                var newCart = new Cart() { UserId = userId };
                await _repository.AddCartAsync(newCart);

                return new CartDto
                {
                    CartId = newCart.Id,
                    TotalPrice = 0,
                    CartItemDto = new List<CartItemDto>()
                };
            }

            var cartDto = new CartDto
            {
                CartId = cart.Id,
                CartItemDto = new List<CartItemDto>(),
                TotalPrice = 0
            };

            foreach (var item in cart.cartItems)
            {
                var cartItemDto = new CartItemDto
                {
                    CartItemId = item.Id,
                    ProductItemId = item.ProductItemId,
                    SizeId = item.SizeId,
                    Quantity = item.Quantity,
                    ProductName = item.ProductItem.Product.ProductName,
                    ColorName = item.ProductItem.Color.ColorName,
                    BrandName = item.ProductItem.Product.Brand.BarndName,
                    SizeName = item.Size.SizeNmae,
                    Price = (item.ProductItem.SalesPrice)*(item.Quantity),
                    ImageUrl = "https://yamanstore.blob.core.windows.net/product-photos/"+ item.ProductItem.ProductImages?.FirstOrDefault()?.ImageFilename
                };

       
                cartDto.TotalPrice += item.ProductItem.SalesPrice * item.Quantity;
                cartDto.CartItemDto.Add(cartItemDto);
            }

            return cartDto;
        }


        public async Task<CartDto> GetGuestCart(List<LocalCartItem> localCart)
        {

            var cartDto = new CartDto
            {
                CartId = 0, 
                CartItemDto = new List<CartItemDto>(),
                TotalPrice = 0
            };

            foreach (var guestItem in localCart)
            {
             
                var productItem = await _repository.GetProductByProductItemIdAsync(guestItem.ProductItemId);

                var size = await _sizeRepository.GetByIdAsync(guestItem.SizeId);

                if (productItem != null && size != null)
                {
                    var cartItemDto = new CartItemDto
                    {
                        CartItemId = guestItem.cartItemId,
                        ProductItemId = guestItem.ProductItemId,
                        SizeId = guestItem.SizeId,
                        Quantity = guestItem.Quantity,

                        ProductName = productItem.Product.ProductName,
                        ColorName = productItem.Color.ColorName,
                        BrandName = productItem.Product.Brand.BarndName,
                        SizeName = size.SizeNmae,
                        Price = (productItem.SalesPrice)*(guestItem.Quantity),
                        ImageUrl = "https://yamanstore.blob.core.windows.net/product-photos/" + productItem.ProductImages?.FirstOrDefault()?.ImageFilename
                    };

                    cartDto.TotalPrice += cartItemDto.Price ;
                    cartDto.CartItemDto.Add(cartItemDto);
                }
            }

            return cartDto;


        }







        public async Task<bool> AddItemToCartAsync(int userId, AddToCartDto AddToCartDto)
        {
            var cart = await _repository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                var newcart = new Cart()
                {
                    UserId = userId,
                };

                await _repository.AddCartAsync(newcart);
            }

            var CartItem = cart.cartItems.FirstOrDefault(x => x.ProductItemId == AddToCartDto.ProductItemId && x.SizeId== AddToCartDto.SizeId);

            if (CartItem != null)
            {
                var updatecart = new UpdateCartItemQuantityDto
                {
                    CartItemId= CartItem.Id,
                    Quantity= AddToCartDto.Quantity,
                    
                };
              
               return  await UpdateCartItemQuantityAsync(updatecart);

    

            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId=cart.Id,
                    ProductItemId= AddToCartDto.ProductItemId,
                    SizeId= AddToCartDto.SizeId,
                    Quantity=1
                };
              await  _repository.AddCartItemAsync(cartItem);
                return true;
            }
          
        }





        public async Task<bool> MigrateCart(CartMigrationRequest cartMigration, int userId)
        {

            var cart = await _repository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                var newcart = new Cart()
                {
                    UserId = userId,
                };

                await _repository.AddCartAsync(newcart);
                cart = newcart;  

            }

            if (cart.cartItems == null)
                cart.cartItems = new List<CartItem>();


            foreach (var item in cartMigration.Items)
            {
                var existing = cart.cartItems.FirstOrDefault(x => x.ProductItemId == item.ProductItemId && x.SizeId == item.SizeId);

                if(existing != null)
                {
                    var cartItem = await _repository.GetCartItemByIdAsync(existing.Id);
                    int stock = await _repository.CheckStock(cartItem.ProductItemId, cartItem.SizeId);

                    if (((cartItem.Quantity) + item.Quantity) >= stock)
                    {
                        cartItem.Quantity = stock;
                        await _repository.UpdateCartItemAsync(cartItem);
                    }
                    else if (((cartItem.Quantity) + item.Quantity) < 1)
                    {
                        cartItem.Quantity = 1;
                        await _repository.UpdateCartItemAsync(cartItem);

                    }
                    else
                    {
                        cartItem.Quantity += item.Quantity;
                        await _repository.UpdateCartItemAsync(cartItem);
                    }

                }
                else
                {
                    var newItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductItemId = item.ProductItemId,
                        SizeId=item.SizeId,
                        Quantity = item.Quantity
                    };

                    await _repository.AddCartItemAsync(newItem);
                }


            }
            return true;


        }







        public async Task<bool> UpdateCartItemQuantityAsync(UpdateCartItemQuantityDto quantityDto)
        {
            var cartItem = await _repository.GetCartItemByIdAsync(quantityDto.CartItemId);

            int stock = await _repository.CheckStock(cartItem.ProductItemId, cartItem.SizeId);


            if(((cartItem.Quantity)+quantityDto.Quantity) >stock )
            {
                cartItem.Quantity = stock;
                await _repository.UpdateCartItemAsync(cartItem);
                return false;

            }
            else if(((cartItem.Quantity) + quantityDto.Quantity) < 1)
            {
                cartItem.Quantity = 1;
                await _repository.UpdateCartItemAsync(cartItem);
                return false;


            }
            else
            {
                cartItem.Quantity += quantityDto.Quantity;
                await _repository.UpdateCartItemAsync(cartItem);
                return true;

            }
        }


        public async Task ClearCartItemAsync(int cartId)
        {
            await _repository.DeleteCartAsync(cartId);
        }


        public async Task DeleteCartItemAsync(int carItemtId)
        {

            await _repository.DeleteCartItemAsync(carItemtId);
        }

        public async Task<int> CheckStockAsync(CheckStoc checkStoc)
        {
            return await _repository.CheckStock(checkStoc.productItemId, checkStoc.sizeId); 
        }

        public async Task<int> CartQuantityAsync(int userId)
        {
            return await _repository.CartQuantityAsync(userId);
            
        }

    }
}
