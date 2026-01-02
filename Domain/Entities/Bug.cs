using BugTrackerAPI.Domain.Enums;
namespace BugTrackerAPI.Domain.Entities
{
    public class Bug
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public BugSeverity Severity { get; set; }
        public BugStatus Status { get; set; } = BugStatus.Open;

        public string CreatedByUserId { get; set; }
        public string? AssignedToUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string FilePath { get; set; }
    }
}
