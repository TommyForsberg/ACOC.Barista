namespace ACOC.Barista.Contracts.servicebus
{
    public class MessageDTO
    {
        public string? Callback { get; set; } = null!;
        public object Body { get; set; } = null!;
    }
}
