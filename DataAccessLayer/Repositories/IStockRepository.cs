using DataAccessLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public  interface IStockRepository
    {
        Task<ProductItemSize> GetStockByIdAsync(int ProductItemSizeId);
        Task<ProductItemSize> GetStockByPZAsync(int ProductItemId,int SizeId);
        Task UpdateStockAsync(ProductItemSize stock);
    }
}
