using BugTrackerAPI.Application.Dtos;

namespace BugTrackerAPI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
