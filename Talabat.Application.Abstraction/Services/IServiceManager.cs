namespace Talabat.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        public IOrderService OrderService { get; } 
        public IProductService ProductService { get; } 
        public IBasketService BasketService { get; } 
        public IAuthService AuthService { get; } 
    }
}
 