namespace Talabat.Domain.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<Basket?> CreateeOrUpdatePaymentIntent(string basketId); 
    }
}
