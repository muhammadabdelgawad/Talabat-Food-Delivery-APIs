
namespace Talabat.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> loginAsync(LoginDto model);

        
        Task<UserDto> RegisterAsync(RegisterDto model);
        
        Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);
        
        Task<AddressDto?> GetUserAddressAsync(ClaimsPrincipal claimsPrincipal); 
        
        Task<AddressDto> UpdateUserAddressAsync(ClaimsPrincipal claimsPrincipal, AddressDto model);

        Task<bool> CheckEmailExists(string email);
    
    
    
    }
}
