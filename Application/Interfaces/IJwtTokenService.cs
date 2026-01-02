using BugTrackerAPI.Domain.Entities;

namespace BugTrackerAPI.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(ApplicationUser user,string role);
    }
}
