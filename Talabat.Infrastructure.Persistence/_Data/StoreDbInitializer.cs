using System.Text.Json;
using Talabat.Infrastructure.Persistence.Common;

namespace Talabat.Infrastructure.Persistence._Data
{
    public class StoreDbInitializer(StoreDbContext _dbContext) : DbInitializer(_dbContext), IStoreDbIntializer
    {
       

        public override async Task SeedAsync()
        {

            if (!_dbContext.Products.Any())
            {
                var productBrands = File.ReadAllText("../Talabat.Infrastructure.Persistence/Data/Seeds/Brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);

                if (brands?.Count > 0)
                {
                    await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }

            }

            if (!_dbContext.Categories.Any())
            {
                var productCategories = File.ReadAllText("../Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(productCategories);

                if (categories?.Count > 0)
                {
                    await _dbContext.Set<ProductCategory>().AddRangeAsync(categories);
                    await _dbContext.SaveChangesAsync();
                }

            }

            if (!_dbContext.Products.Any())
            {
                var productData = File.ReadAllText("../Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
                var peoducts = JsonSerializer.Deserialize<List<Product>>(productData);

                if (peoducts?.Count > 0)
                {
                    await _dbContext.Set<Product>().AddRangeAsync(peoducts);
                    await _dbContext.SaveChangesAsync();
                }

            }
        }
    }
}
