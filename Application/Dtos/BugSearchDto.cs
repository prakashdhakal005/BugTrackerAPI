using BugTrackerAPI.Domain.Enums;

namespace BugTrackerAPI.Application.Dtos
{
    public class BugSearchDto
    {
        public string? Title { get; set; }
        public BugSeverity? Severity { get; set; }
        public BugStatus? Status { get; set; }
    }
}
