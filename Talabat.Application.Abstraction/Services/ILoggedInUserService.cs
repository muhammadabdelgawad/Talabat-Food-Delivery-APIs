namespace Talabat.Application.Abstraction.Services
{
    public interface ILoggedInUserService
    {
        string? UserId { get; }
        string? UserName { get; }
        bool IsAuthenticated { get; }
    }
}