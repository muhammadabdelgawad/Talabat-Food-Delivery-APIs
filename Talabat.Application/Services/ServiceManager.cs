namespace Talabat.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly IConfiguration _configuration;
        private readonly Lazy<IAuthService> _authService;
        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper,
            IConfiguration configuration,Func<IBasketService> basketServiceFactory,
            Func<IAuthService> authServiceFactory)
        {
            //_configuration=configuration;
            _unitOfWork =unitOfWork;
            _mapper=mapper; 
            _productService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _authService = new Lazy<IAuthService>(authServiceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IProductService ProductService  =>_productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;
    }
}
 