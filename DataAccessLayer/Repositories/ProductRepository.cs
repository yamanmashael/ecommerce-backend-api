using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using FuzzySharp;


namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

  

    public async Task<List<string>> GetSearchSuggestionsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<string>();

            var words = query.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var productNames = await _context.Product.Select(p => p.ProductName).ToListAsync();
            var colorNames = await _context.Color.Select(c => c.ColorName).ToListAsync();
            var categoryItemNames = await _context.CategoryItem.Select(ci => ci.CategoryItemName).ToListAsync();
            var brandNames = await _context.Brand.Select(b => b.BarndName).ToListAsync();
            var GenderName = await _context.Gender.Select(g=>g.GenderName).ToListAsync();
            var CategoryName = await _context.Category.Select(g => g.CategoryName).ToListAsync();


            var allCandidates = productNames
                .Union(colorNames)
                .Union(categoryItemNames)
                .Union(brandNames)
                .Union(GenderName)
                .Union(CategoryName)
                .Distinct()
                .ToList();

            var suggestions = allCandidates
                .Select(x => new
                {
                    Value = x,
                    Score = words.Max(word => Fuzz.PartialRatio(x.ToLower(), word))
                })
                .Where(x => x.Score > 46) 
                .OrderByDescending(x => x.Score)
                .Take(10)
                .Select(x => x.Value)
                .ToList();

            return suggestions;
        }




    public async Task<IEnumerable<ProductItem>> FilterProductsAsync(
            List<int>? genderId,
            List<int>? categoryId,
            List<int>? categoryItemId,
            List<int>? brandId,
            List<int>? sizeId,
            List<int>? colorId,
            decimal? minPrice,
            decimal? maxPrice,
            string? keyword)
        {
            var query = _context.ProductItem
         .Include(p => p.Product)
             .ThenInclude(b => b.Brand)
         .Include(p => p.Product)
             .ThenInclude(ci => ci.CategoryItem)
                 .ThenInclude(c => c.Category)
                     .ThenInclude(g => g.Gender)
         .Include(c => c.Color)
         .Include(i => i.ProductImages)
         .Include(pis => pis.ProductItemSizes)
             .ThenInclude(s => s.Size)
         .AsQueryable();

            if (genderId?.Any() == true)
                query = query.Where(p => genderId.Contains(p.Product.CategoryItem.Category.GenderId));

            if (categoryId?.Any() == true)
                query = query.Where(p => categoryId.Contains(p.Product.CategoryItem.CategoryId));

            if (categoryItemId?.Any() == true)
                query = query.Where(p => categoryItemId.Contains(p.Product.CategoryItemId));

            if (brandId?.Any() == true)
                query = query.Where(p => brandId.Contains(p.Product.BrandId));

            if (colorId?.Any() == true)
                query = query.Where(p => colorId.Contains(p.ColorId));

            if (sizeId?.Any() == true)
                query = query.Where(p => p.ProductItemSizes.Any(ps => sizeId.Contains(ps.SizeId)));

            if (minPrice.HasValue)
                query = query.Where(p => p.SalesPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.SalesPrice <= maxPrice.Value);

            var result = await query.ToListAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var words = keyword.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                result = result
                    .Select(p => new
                    {
                        Item = p,
                        
                        Score = words.Max(word => Math.Max(
                                    Math.Max(Fuzz.PartialRatio(p.Product.ProductName.ToLower(), word),
                                             Fuzz.PartialRatio(p.Product.ProductDescription.ToLower(), word)),
                                    Math.Max(Fuzz.PartialRatio(p.Product.CategoryItem.CategoryItemName.ToLower(), word),
                                             Math.Max(Fuzz.PartialRatio(p.Product.CategoryItem.Category.CategoryName.ToLower(), word),
                                                      Math.Max(Fuzz.PartialRatio(p.Product.Brand.BarndName.ToLower(), word),
                                                               Fuzz.PartialRatio(p.Product.CategoryItem.Category.Gender.GenderName.ToLower(), word)
                                                               )
                                                      )
                                             )
                                    ))
                    })
                    .Where(x => x.Score > 55) 
                    .OrderByDescending(x => x.Score)
                    .Select(x => x.Item)
                    .ToList();
            }

            return result;
        }


        public async Task<IEnumerable<ProductItem>> GetPopularProductsAsync()
        {
            var topProductIds = await _context.OrderDetails
                .GroupBy(od => od.ProductItemId)
                .Select(g => new
                {
                    ProductItemId = g.Key,
                    TotalQuantitySold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .Take(10)
                .Select(x => x.ProductItemId)
                .ToListAsync();
 
            var topProducts = await _context.ProductItem
                .Where(pi => topProductIds.Contains(pi.Id))
                .Include(p => p.Product)
                    .ThenInclude(b => b.Brand)
                .Include(p => p.ProductImages)
                .Include(c => c.Color)
                .ToListAsync();

            return topProducts;
        }




        public async Task<IEnumerable<ProductItem>> GetNewProductsAsync()
        {
            var newProducts = await _context.ProductItem
                .Include(p => p.Product)
                    .ThenInclude(b => b.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.Color)
                .OrderByDescending(p => p.CreatedDate)  
                .Take(10) 
                .ToListAsync();

            return newProducts;
        }




        public async Task<IQueryable<Product>> GetProductsAsync(int? genderId, int? categoryId, int? categoryItemId, int? BrandId, string search)
        {
            var query =  _context.Product
                .Include(p => p.CategoryItem)
                    .ThenInclude(ci => ci.Category)
                        .ThenInclude(c => c.Gender)
                .Include(b => b.Brand)
                .Include(t=>t.ProductItems).ThenInclude(m=>m.ProductImages)
                .AsQueryable();

            if (BrandId.HasValue)
                query = query.Where(p => p.BrandId == BrandId.Value);

            if (genderId.HasValue)
                query = query.Where(p => p.CategoryItem.Category.GenderId == genderId.Value);

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryItem.CategoryId == categoryId.Value);

            if (categoryItemId.HasValue)
                query = query.Where(p => p.CategoryItemId == categoryItemId.Value);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.ProductName.Contains(search) ||
                                         p.ProductDescription.Contains(search));

            return await  Task.FromResult(query);
        }



        public async Task<Product> GetProductDatailesByIdAsync(int id)
        {
            return await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.CategoryItem)
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.Color)
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.ProductImages)
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.ProductItemSizes)
                        .ThenInclude(pis => pis.Size)
                .FirstOrDefaultAsync(p => p.Id == id);
        }





        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Product
                .Include(i => i.CategoryItem)
                    .ThenInclude(c => c.Category)
                        .ThenInclude(g => g.Gender)
                .Include(b => b.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task AddASync(Product product)
        {


            _context.Product.Add(product);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
        }



 



        public async Task DeleteAsync(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }


    }
}
