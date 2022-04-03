namespace ACOC.Barista.Contracts.http
{
    public class MakeOrderDTO
    {
        public string ProductId { get; set; } = null!;
        public string? Webhook { get; set; }
        public bool ActivateNow { get; set; }
    }
}
