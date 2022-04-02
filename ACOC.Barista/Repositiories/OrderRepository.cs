using ACOC.Barista.Models;
using ACOC.Barista.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ACOC.Barista.Repositiories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly IMongoCollection<Order> _collection;

        public OrderRepository(IMongoClient mongoClient,
            IOptions<BaristaDatabaseSettings> baristaDatabaseSettings)
        {

            var mongoDatabase = mongoClient.GetDatabase(
                baristaDatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<Order>(
                baristaDatabaseSettings.Value.ActiveOrdersCollectionName);
        }

        public async Task<List<Order>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Order?> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order newProduct) =>
            await _collection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Order updatedProduct) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);


        public async Task<IEnumerable<Order>> GetByFilter(Expression<Func<Order, bool>> filter)
        {
            var bsonFilter = Builders<Order>.Filter.Where(filter);
           return await _collection.Find(bsonFilter).ToListAsync();
        }

        public async Task<Order> GetFirstOrDefaultByFilter(Expression<Func<Order, bool>> filter)
        {
            var bsonFilter = Builders<Order>.Filter.Where(filter);
            return await _collection.Find(bsonFilter).FirstOrDefaultAsync();
        }
    }
}
