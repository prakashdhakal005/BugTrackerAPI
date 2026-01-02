using BugTrackerAPI.Application.Interfaces;
using BugTrackerAPI.Domain.Entities;
using BugTrackerAPI.Domain.Enums;
using BugTrackerAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerAPI.Infrastructure.Repository
{
    public class BugRepository : IBugRepository
    {
        private readonly ApplicationDbContext _context;

        public BugRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Bug bug)
        {
            await _context.Bugs.AddAsync(bug);
        }

        public async Task<Bug?> GetByIdAsync(int id)
        {
            return await _context.Bugs
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Bug>> SearchUnassignedAsync(
            string? title,
            BugSeverity? severity,
            BugStatus? status)
        {
            IQueryable<Bug> query = _context.Bugs
                .Where(b => b.AssignedToUserId == null);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (severity.HasValue)
                query = query.Where(b => b.Severity == severity);

            if (status.HasValue)
                query = query.Where(b => b.Status == status);

            return await query
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
        public async Task<List<Bug>> GetAllAsync()
        {
            return await _context.Bugs
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
