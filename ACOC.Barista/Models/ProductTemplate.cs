using ACOC.Barista.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ACOC.Barista.Models
{
    public class ProductTemplate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        public string? Category { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ProductType Type { get; set; }

        public virtual ICollection<LifeCycleEvent> FutureEvents { get; set; } = null!;
    }
}
