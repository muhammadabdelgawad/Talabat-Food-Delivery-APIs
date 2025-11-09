
namespace Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) :BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var result = await serviceManager.AuthService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model) 
        {
            var result = await serviceManager.AuthService.loginAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser() 
        {
            var result = await serviceManager.AuthService.GetCurrentUserAsync(User);
            return Ok(result);
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var result = await serviceManager.AuthService.GetUserAddressAsync(User);
            return Ok(result);
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult> UpdateUserAddress(AddressDto addressDto)
        {
            var result = await serviceManager.AuthService.UpdateUserAddressAsync(User, addressDto);
            return Ok(result);
        }

        [HttpGet("emailexisits")]
       
        public async Task<ActionResult> CheckEmailExists(string email)
        {
            return Ok(await serviceManager.AuthService.CheckEmailExists(email));
        }










    }
}
 