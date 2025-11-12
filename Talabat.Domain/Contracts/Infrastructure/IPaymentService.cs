
namespace Talabat.Domain.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId); 
    }
}
