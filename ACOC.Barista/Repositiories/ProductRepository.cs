using ACOC.Barista.Models;
using ACOC.Barista.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ACOC.Barista.Repositiories
{
    public class ProductRepository : IRepository<ProductTemplate>
    {
        private readonly IMongoCollection<ProductTemplate> _productCollection;

        public ProductRepository(IMongoClient mongoClient,
            IOptions<BaristaDatabaseSettings> baristaDatabaseSettings)
        {

            var mongoDatabase = mongoClient.GetDatabase(
                baristaDatabaseSettings.Value.DatabaseName);

            _productCollection = mongoDatabase.GetCollection<ProductTemplate>(
                baristaDatabaseSettings.Value.ProductsCollectionName);
        }

        public async Task<List<ProductTemplate>> GetAsync() =>
            await _productCollection.Find(_ => true).ToListAsync();

        public async Task<ProductTemplate?> GetAsync(string id) =>
            await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<ProductTemplate?> GetByNameAsync(string name) =>
        await _productCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<string> CreateAsync(ProductTemplate newProduct) {
            await _productCollection.InsertOneAsync(newProduct);
            return newProduct.Id;
    }

        public async Task UpdateAsync(string id, ProductTemplate updatedProduct) =>
            await _productCollection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await _productCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<IEnumerable<ProductTemplate>> GetByFilter(Expression<Func<ProductTemplate, bool>> filter)
        {
            var bsonFilter = Builders<ProductTemplate>.Filter.Where(filter);
            return await _productCollection.Find(bsonFilter).ToListAsync();
        }

        public async Task<ProductTemplate> GetFirstOrDefaultByFilter(Expression<Func<ProductTemplate, bool>> filter)
        {
            var bsonFilter = Builders<ProductTemplate>.Filter.Where(filter);
            return await _productCollection.Find(bsonFilter).FirstOrDefaultAsync();
        }
    }
}
