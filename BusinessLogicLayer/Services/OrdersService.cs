using Azure.Core;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICartRepository _cartRepository ;
        private readonly IAddressRepository _addressRepository;
        private readonly IProductItemRepository _productItemRepository ; 
        private readonly IStockRepository _stockRepository ;
        public OrdersService(IOrdersRepository ordersRepository, IStockRepository stockRepository ,  ICartRepository cartRepository, IAddressRepository addressRepository, IProductItemRepository productItemRepository)
        {
            _ordersRepository = ordersRepository;   
            _cartRepository = cartRepository;
            _addressRepository = addressRepository; 
            _productItemRepository = productItemRepository; 
            _stockRepository=stockRepository;   
        }


        public async Task<ResponseOrder<IEnumerable<OrderDto>>> GetOrdersDashboard(RequestOrder dto)
        {
            var query =await _ordersRepository.GetOrdersDashboardAsync(dto.OrderId,dto.Status,dto.PaymentMethod,dto.ShippingCompany,dto.StartDate,dto.EndDate);

          

            int totalCount = await query.CountAsync();
            decimal total = await query.SumAsync(x => x.TotalPrice);


            int totalPages = (int)Math.Ceiling(totalCount / (double)dto.PageSize);
            if (dto.PageNumber > totalPages)
                dto.PageNumber = totalPages > 0 ? totalPages : 1;

            var Orders = await query
            .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            var item = Orders.Select(x => new OrderDto
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                PaymentMethod = x.PaymentMethod,
                ShippingCompany = x.ShippingCompany,
                PaymentStatus = x.PaymentStatus,
                Status = x.OrderStatus.StatusName,
                TotalPrice = x.TotalPrice,
                TrackingNumber = x.TrackingNumber
            });



            return new ResponseOrder<IEnumerable<OrderDto>>
            {
                TotalCountOrder= totalCount,
               TotalPrice = total,

                Pending = await query.CountAsync(x => x.OrderStatusId == 1),
                Processing = await query.CountAsync(x => x.OrderStatusId == 2),
                Shipped = await query.CountAsync(x => x.OrderStatusId == 3),
                Completed = await query.CountAsync(x => x.OrderStatusId == 4),
                Canceled = await query.CountAsync(x => x.OrderStatusId == 5),
                Returned = await query.CountAsync(x => x.OrderStatusId == 6),


                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalPages = totalPages,

                Success = true,
                ErrorMassage = null,
                Data = item

            };

        }


        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int userId)
        {
            var orders = await _ordersRepository.GetOrdersAsync(userId);    

            return orders.Select(x => new OrderDto
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                PaymentMethod = x.PaymentMethod,
                ShippingCompany = x.ShippingCompany,
                PaymentStatus = x.PaymentStatus,
                Status = x.OrderStatus.StatusName,
                TotalPrice = x.TotalPrice,
                TrackingNumber = x.TrackingNumber
            });
        }



        public async Task<IEnumerable<OrderDetailsDto>> GetOrderDatailsAsync(int OrderId)
        {
            var order = await _ordersRepository.GetOrderDetailsAsync(OrderId);

            var result = order.OrderDetails.Select(x => new OrderDetailsDto
            {
                Id = x.Id,
                Adress=x.Orders.Adress,
                Quantity = x.Quantity,
                SizeName = x.Size.SizeNmae,
                ProductName = x.ProductItem.Product.ProductName,
                ImageUrl = "https://yamanstore.blob.core.windows.net/product-photos/" + x.ProductItem.ProductImages.FirstOrDefault()?.ImageFilename,
                Price = x.ProductItem.Price
            });


            return result;

    }

     


        public async Task CreateOrdersAsync(int AdressId ,int UserId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(UserId);
            var adres = await _addressRepository.GetAddressByIdAsync(AdressId, UserId);



            var order = new Orders()
            {
                UserId = UserId,
                OrderDate = DateTime.Now,

                PaymentMethod = "Cash",
                PaymentStatus = false,
                ShippingCost = "0",
                OrderStatusId = 2,
                Adress = adres.FulName + "-" + adres.PhoneNumber + "-" + adres.City + "-" + adres.FullAddress,
                OrderDetails = new List<OrderDetails>(),
                TotalPrice = 0

            };


            foreach (var item in cart.cartItems)
            {
                var productItem = await _productItemRepository.GetProductItemByIdAsync(item.ProductItemId);
                var orderItem = new OrderDetails()
                {
                    ProductItemId = item.ProductItemId,
                    Quantity = item.Quantity,
                    SizeId = item.SizeId,
                    Price = productItem.SalesPrice
                };

                order.TotalPrice += item.Quantity * productItem.SalesPrice;
                order.OrderDetails.Add(orderItem);

                var stock = await _stockRepository.GetStockByPZAsync(item.ProductItemId, item.SizeId);
                stock.Stock -= item.Quantity;
                await _stockRepository.UpdateStockAsync(stock); 


            }


            await _ordersRepository.AddOrdersAsync(order);
            await _cartRepository.DeleteCartAsync(cart.Id);

        }

        public async Task UpdateShippingAsync(UpdateShippingDto updateShipping)
        {
            var order = await _ordersRepository.GetOrderByIdAsync(updateShipping.OrderId);

            order.ShippingCompany = updateShipping.ShippingCompany;
            order.TrackingNumber = updateShipping.TrackingNumber;

            await _ordersRepository.UpdateOrdersAsync(order);
        }

        public async Task UpdatePaymentStatusAsync(UpdatePaymentStatusDto updatePaymentStatusDto)
        {
            var order = await _ordersRepository.GetOrderByIdAsync(updatePaymentStatusDto.OrderId);
            order.PaymentStatus = updatePaymentStatusDto.PaymentStatus;
            await _ordersRepository.UpdateOrdersAsync(order);
        }

        public async Task UpdateOrderStatusAsync(UpdateStatusDto orderStatus)
        {
            var order = await _ordersRepository.GetOrderByIdAsync(orderStatus.OrderId);
            order.OrderStatusId = orderStatus.OrderStatusId;
            await _ordersRepository.UpdateOrdersAsync(order);
        }

     

        public async Task CancelOrderAsync(int OrderId)
        {

            var order = await _ordersRepository.GetOrderByIdAsync(OrderId);

             if(order.OrderStatus.StatusName != "Shipped")
            {
                foreach (var item in order.OrderDetails)
                {
                    var stock = await _stockRepository.GetStockByPZAsync(item.ProductItemId, item.SizeId);
                    stock.Stock += item.Quantity;
                    await _stockRepository.UpdateStockAsync(stock);
                }

                order.OrderStatusId = 5;
                await _ordersRepository.UpdateOrdersAsync(order);


            }


         }



    }
}
