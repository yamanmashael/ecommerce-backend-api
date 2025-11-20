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
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _repository;

        public GenderService(IGenderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GenderDto>> GetAllAsync()
        {
            var genders = await _repository.GetAllAsync();
            return genders.Select(g => new GenderDto
            {
                Id = g.Id,
                GenderName = g.GenderName
            });
        }

        public async Task<GenderDto> GetByIdAsync(int id)
        {
            var gender = await _repository.GetByIdAsync(id);
            if (gender == null) return null;

            return new GenderDto
            {
                Id = gender.Id,
                GenderName = gender.GenderName
            };
        }

        public async Task CreateAsync(CreateGenderDto dto)
        {
            var gender = new Gender
            {
                GenderName = dto.GenderName
            };

            await _repository.AddAsync(gender);
        }

        public async Task UpdateAsync(int id, UpdateGenderDto dto)
        {
            var gender = await _repository.GetByIdAsync(id);
            if (gender == null) throw new Exception("Gender not found");

            gender.GenderName = dto.GenderName;
            await _repository.UpdateAsync(gender);
        }

        public async Task DeleteAsync(int id)
        {
            var gender = await _repository.GetByIdAsync(id);
            if (gender == null) throw new Exception("Gender not found");

            await _repository.DeleteAsync(gender);
        }
    }

}
