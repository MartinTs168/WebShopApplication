using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopApp.Infrastructure.Data.Entities;

namespace WebShopApp.Core.Contracts
{
    public interface IBrandService
    {
        Task<List<Brand>> GetBrandsAsync();
        Task<Brand> GetBrandByIdAsync(int brandId);
        Task<List<Product>> GetProductsByBrandAsync(int brandId);
    }
}
