using ACOC.Barista.Models;

namespace ACOC.Barista.Contracts.http
{
    public class LifeCycleEventDTO
    {
        public string? EventType { get; set; }
        public string? Value { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public Interval TimeSpan { get; set; } = null!;
    }
}