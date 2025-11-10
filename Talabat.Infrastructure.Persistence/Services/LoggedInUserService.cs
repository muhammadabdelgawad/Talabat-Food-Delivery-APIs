using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Talabat.Application.Abstraction.Services;

namespace Talabat.Infrastructure.Persistence.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) 
                                ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue("sub")
                                ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");

        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)
                                  ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue("name")
                                  ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}