using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _db;
        public ProductRepository(StoreContext db)
        {
            _db = db;
        }



        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync( p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var typeId = 1;

            var products = _db.Products.Where(x => x.ProductTypeId == typeId).Include(p => p.ProductType).ToListAsync();
            
            return await _db.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _db.ProductBrands.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _db.ProductTypes.ToListAsync();
        }
    }
}