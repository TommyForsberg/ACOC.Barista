namespace ACOC.Barista.Models
{
    public class LifeCycleEvent
    {
        public LifeCycleEvent()
        {

        }
        public LifeCycleEvent(LifeCycleEvent e, DateTime dateTime)
        {
            Title = e.Title;
            Description = e.Description;
            Date = dateTime.Add(new TimeSpan(days: e.TimeSpan.Days, hours: e.TimeSpan.Hours, minutes: e.TimeSpan.Minutes, seconds: 0));
            EventType = e.EventType;
            Value = e.Value;
            HasOccured = false;
        }
        public string? EventType { get; set; }
        public string? Value { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public Interval TimeSpan { get; set; } = null!;
        public bool HasOccured { get; set; }
    }
}
