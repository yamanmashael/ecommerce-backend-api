using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _repository;

        public OrderStatusService(IOrderStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderStatusDto>> GetAllAsync()
        {
            var statuses = await _repository.GetAllAsync();
            return statuses.Select(s => new OrderStatusDto
            {
                Id = s.Id,
                StatusName = s.StatusName
            });
        }

        public async Task<OrderStatusDto> GetByIdAsync(int id)
        {
            var status = await _repository.GetByIdAsync(id);
            if (status == null) return null;

            return new OrderStatusDto
            {
                Id = status.Id,
                StatusName = status.StatusName
            };
        }

        public async Task<OrderStatusDto> CreateAsync(CreateOrderStatusDto dto)
        {
            var status = new OrderStatus { StatusName = dto.StatusName };
            await _repository.AddAsync(status);

            return new OrderStatusDto
            {
                Id = status.Id,
                StatusName = status.StatusName
            };
        }

        public async Task<OrderStatusDto> UpdateAsync(UpdateOrderStatusDto dto)
        {
            var status = await _repository.GetByIdAsync(dto.Id);
            if (status == null) return null;

            status.StatusName = dto.StatusName;
            await _repository.UpdateAsync(status);

            return new OrderStatusDto
            {
                Id = status.Id,
                StatusName = status.StatusName
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _repository.GetByIdAsync(id);
            if (status == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }

}
