using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ACOC.Barista.Models
{
    public class Order
    {
        public Order(ProductTemplate orderBluePrint)
        {
            OrderDateTime = DateTime.UtcNow;
            Product = new Product(orderBluePrint, OrderDateTime);
            FriendlyUUID = Guid.NewGuid().ToString();
            ReadyDateTime = DateTime.UtcNow.AddMinutes(5);
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string FriendlyUUID { get; set; }
        public Product Product { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime ReadyDateTime { get; set; }
        public string? Webhook { get; set; }
    }
}
