using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopApp.Core.Contracts;
using WebShopApp.Infrastructure.Data;
using WebShopApp.Infrastructure.Data.Entities;

namespace WebShopApp.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<Brand> GetBrandByIdAsync(int brandId)
        {
            return await _context.Brands.FindAsync(brandId);
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();
            return brands;
        }

        public async Task<List<Product>> GetProductsByBrandAsync(int brandId)
        {
            return await _context.Products
                .Where(p =>  p.BrandId == brandId)
                .ToListAsync();
        }
    }
}
