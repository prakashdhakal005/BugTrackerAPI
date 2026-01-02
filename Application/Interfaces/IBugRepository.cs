using BugTrackerAPI.Domain.Entities;
using BugTrackerAPI.Domain.Enums;

namespace BugTrackerAPI.Application.Interfaces
{
    public interface IBugRepository
    {
        Task AddAsync(Bug bug);
        Task<Bug?> GetByIdAsync(int id);
        Task<List<Bug>> SearchUnassignedAsync(
            string? title,
            BugSeverity? severity,
            BugStatus? status);

        Task SaveChangesAsync();
        Task<List<Bug>> GetAllAsync();

    }
}
