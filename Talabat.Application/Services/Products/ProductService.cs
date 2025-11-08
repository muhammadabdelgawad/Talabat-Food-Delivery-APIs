namespace Talabat.Application.Services.Products
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId,
                specParams.CategoryId, specParams.PageIndex, specParams.PageSize, specParams.Search);

            var products = await unitOfWork.GetRepositiry<Product, int>().GetAllWithSpecAsync(specs);

            var countSpecs = new ProductWithBrandAndCategorySpecifications(specParams.BrandId,
                specParams.CategoryId,specParams.Search);

            var count = await unitOfWork.GetRepositiry<Product, int>().GetCountAsync(countSpecs);

            var data = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize,count) { Data = data };
        }
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var Product = await unitOfWork.GetRepositiry<Product, int>().GetWithSpecAsync(spec);

            if (Product == null)
                throw new NotFoundException(nameof(Product) , id);

            var productToReturn = mapper.Map<ProductToReturnDto>(Product);
            return productToReturn;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {

            var brands = await unitOfWork.GetRepositiry<ProductBrand, int>().GetAllAsync();
            var brandToReturn = mapper.Map<IEnumerable<BrandDto>>(brands);
            return brandToReturn;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.GetRepositiry<ProductCategory, int>().GetAllAsync();
            var categoryToReturn = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryToReturn;
        }


    }
}
