using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public  interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAsync();

        Task<Brand> GetByIdAsync(int id);
        Task AddAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(Brand brand);

        Task<int> CountAsync();
        Task<int> SerchCountAsync(string? search);



    }
}
