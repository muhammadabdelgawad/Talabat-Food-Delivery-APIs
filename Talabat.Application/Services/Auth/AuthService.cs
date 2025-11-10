using UserAddress = Talabat.Domain.Entities.Identity.UserAddress;

namespace Talabat.Application.Services.Auth
 {
    public class AuthService(
        IMapper mapper,
        IOptions<JwtSettings> jwtSettings,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)  : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        public async Task<UserDto> loginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null) throw new UnAuthorizedException("Invalid Login");

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (result.IsNotAllowed)  throw new UnAuthorizedException("Please Confirm Your Account");

            if (result.IsLockedOut) throw new UnAuthorizedException("Account Is Locked");

            if (!result.Succeeded) throw new UnAuthorizedException("Invalid Login");

            var reponse = new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };
            return reponse;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var existingUser = await userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new ValidationException() { Errors = new[] { $"Email '{model.Email}' is already registered" } };
            }

            
            existingUser = await userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                throw new ValidationException() { Errors = new[] { $"Username '{model.UserName}' is already taken" } };
            }           

            var user = new ApplicationUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}").ToList();
                
                throw new ValidationException()
                {
                    Errors = errors
                };
            }
            
            var response = new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };
            return response;
        }
         
        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var rolesAsCalaim = new List<Claim>();
            
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                rolesAsCalaim.Add(new Claim(ClaimTypes.Role, role.ToString()));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.GivenName,user.DisplayName!),
            }.Union(userClaims)
             .Union(rolesAsCalaim);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: claims,
                signingCredentials: signingCredentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);  

        }

        public async Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            var user= await userManager.FindByEmailAsync(email!);
            return new UserDto
            {
                Id = user!.Id,
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<AddressDto?> GetUserAddressAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.FindUserWithAddress(claimsPrincipal);
           
            var address= mapper.Map<AddressDto>(user!.Address);
              
            return address;
        }

        public async Task<AddressDto> UpdateUserAddressAsync(ClaimsPrincipal claimsPrincipal, AddressDto addressDto)
        {
            var updatedAddress = mapper.Map<UserAddress>(addressDto);

            var user = await  userManager.FindUserWithAddress(claimsPrincipal);

            if (user.Address is not null)
                updatedAddress.Id = user.Address.Id;

            user!.Address = updatedAddress;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(error => error.Description)
                                                                              .Aggregate((x, y) => $"{x},{y}"));
            return addressDto;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await userManager.FindByEmailAsync(email!) is not null;
        }
    }
}
        