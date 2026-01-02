using BugTrackerAPI.Domain.Enums;

namespace BugTrackerAPI.Application.Dtos
{
    public class BugCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public BugSeverity Severity { get; set; }
        public string FilePath { get; set; }
    }
}
