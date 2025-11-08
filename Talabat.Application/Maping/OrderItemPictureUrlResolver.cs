using Talabat.Domain.Entities.Orders;

namespace Talabat.Application.Maping
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string?>
    {
        private readonly IConfiguration configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product?.PictureUrl))
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.Product.PictureUrl}";

            return string.Empty;
        }
    }
}
