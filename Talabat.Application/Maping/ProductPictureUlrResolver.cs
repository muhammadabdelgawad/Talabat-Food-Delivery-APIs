namespace Talabat.Application.Maping
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string?>
    {
        private readonly IConfiguration configuration;
        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string? Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";

            return string.Empty;
            
        }
    }
}
