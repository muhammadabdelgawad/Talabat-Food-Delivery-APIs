namespace Talabat.Domain.Contracts.Infrastructure
{
    public interface IBasketRepository
    {
        Task<Basket?> GetAsync(string id);
        Task<Basket?> UpdateAsync(Basket basket, TimeSpan timeToLive);
        Task DeleteAsync(string id);

    }
}
