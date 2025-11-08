namespace Talabat.Infrastructure.Persistence.Repositories.Baskets
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper, IConfiguration configuration) : IBasketService
    {
        private readonly IBasketRepository _basketRepo = basketRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration; 

        public async Task<BasketDto> GetCustomerBasket(string id)
        {
              var basket= await _basketRepo.GetAsync(id);
            if(basket is null) throw new NotFoundException(nameof(Basket), id);
            var mappedBasket = _mapper.Map<BasketDto>(basket);
            return mappedBasket;
        }

        public async Task<BasketDto> UpdateCustomerBasket(BasketDto basket) 
        {
             var mappedBasket = _mapper.Map<Basket>(basket);
             var daysToLive = int.Parse(_configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!);
             var updatedBasket = await _basketRepo.UpdateAsync(mappedBasket,
                    TimeSpan.FromDays(daysToLive));
            if (updatedBasket is null) throw new BadRequestException
                 ("An Error has occurred ,Cannot update you Basket . please try again ");

            return basket;
        }

        public async Task DeleteCustomerBasketAsync(string id)
            => await _basketRepo.DeleteAsync(id);

    }
}
