namespace ACOC.Barista.Models.Settings
{
    public class BaristaDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProductsCollectionName { get; set; } = null!;
        public string OrderHistoryCollectionName { get; set; } = null!;
        public string ActiveOrdersCollectionName { get; set; } = null!;     
    }
}