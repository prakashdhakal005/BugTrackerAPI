using BugTrackerAPI.Application.Dtos;
using BugTrackerAPI.Domain.Entities;
using BugTrackerAPI.Domain.Enums;

namespace BugTrackerAPI.Application.Interfaces
{
    public interface IBugService
    {
        Task<int> CreateBugAsync(BugCreateDto dto, string userId);
        Task UpdateBugStatusAsync(int bugId, BugStatus status, string developerId);
        Task<List<Bug>> SearchUnassignedBugsAsync(BugSearchDto dto);
        Task<List<Bug>> GetAllBugsAsync();

    }
}
