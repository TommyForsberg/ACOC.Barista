using ACOC.Barista.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ACOC.Barista.Models
{
    public class Product: ProductTemplate
    {
        public Product(ProductTemplate orderBluePrint, DateTime dateTime)
        {
            Name = orderBluePrint.Name; 
            State = LifeCycleState.Created;
            FutureEvents = orderBluePrint.FutureEvents.Select(e => new LifeCycleEvent(e, dateTime)).ToList();
            Type = orderBluePrint.Type;
            Description = orderBluePrint.Description;
        }
 
        [BsonRepresentation(BsonType.String)]
        public LifeCycleState State { get; set; }
        public virtual ICollection<LifeCycleEvent> Events { get; set; } = new List<LifeCycleEvent>();
    }

}
