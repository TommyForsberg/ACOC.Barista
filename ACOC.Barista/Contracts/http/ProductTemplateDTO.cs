namespace ACOC.Barista.Contracts.http
{
    public class ProductTemplateDTO
    {
        public string Name { get; set; } = null!;

        public string? Type { get; set; }

        public virtual ICollection<LifeCycleEventDTO> FutureEvents { get; set; } = null!;
    }
   
}
