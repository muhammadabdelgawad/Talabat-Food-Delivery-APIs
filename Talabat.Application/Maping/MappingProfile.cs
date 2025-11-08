namespace Talabat.Application.Maping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductToReturnDto>()
                  .ForMember(d => d.Brand, O => O.MapFrom(src => src.Brand!.Name))
                  .ForMember(d => d.Category, O => O.MapFrom(src => src.Category!.Name))
                  .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUlrResolver>());   
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();

            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Domain.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod!.ShortName));



        }
    }
}
 