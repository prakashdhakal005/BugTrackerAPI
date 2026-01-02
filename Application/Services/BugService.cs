using BugTrackerAPI.Application.Dtos;
using BugTrackerAPI.Application.Interfaces;
using BugTrackerAPI.Domain.Entities;
using BugTrackerAPI.Domain.Enums;
using BugTrackerAPI.Infrastructure.Data;
using BugTrackerAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerAPI.Application.Services
{
    public class BugService: IBugService
    {
        private readonly IBugRepository _bugRepository;

        public BugService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        public async Task<int> CreateBugAsync(BugCreateDto dto, string userId)
        {
            var bug = new Bug
            {
                Title = dto.Title,
                Description = dto.Description,
                FilePath=dto.FilePath,
                Severity = dto.Severity,
                CreatedByUserId = userId
            };

            await _bugRepository.AddAsync(bug);
            await _bugRepository.SaveChangesAsync();

            return bug.Id;
        }
        public async Task UpdateBugStatusAsync(
            int bugId,
            BugStatus status,
            string developerId)
        {
            var bug = await _bugRepository.GetByIdAsync(bugId);

            if (bug == null)
                throw new Exception("Bug not found");

            bug.Status = status;
            bug.AssignedToUserId ??= developerId;

            await _bugRepository.SaveChangesAsync();
        }
        public async Task<List<Bug>> SearchUnassignedBugsAsync(BugSearchDto dto)
        {
            return await _bugRepository.SearchUnassignedAsync(
                dto.Title,
                dto.Severity,
                dto.Status);
        }

        public async Task<List<Bug>> GetAllBugsAsync()
        {
            return await _bugRepository.GetAllAsync();
        }

    }
}
