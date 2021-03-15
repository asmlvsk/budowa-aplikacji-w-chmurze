using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIAwC.Data
{
    public class MongoDbContext
    {
        private readonly IMongoCollection<DbItem> items;

        public MongoDbContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            items = database.GetCollection<DbItem>("Items");
        }

        public List<DbItem> GetAllItems()
        {
            try
            {
                return items.Find(_ => true).ToList();
            }
            catch
            {

                throw;
            }
        }

        // Create
        public async Task AddItem(DbItem item)
        {
            try
            {
                await items.InsertOneAsync(item);
            }
            catch
            {

                throw;
            }
        }

        // Read
        public async Task<DbItem> GetItemData(string itemId)
        {
            try
            {
                FilterDefinition<DbItem> filterDefinition = Builders<DbItem>.Filter.Eq("Id", itemId);
                return await items.Find(filterDefinition).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }

        // Update
        public void UpdateItem(DbItem item)
        {
            try
            {
                items.ReplaceOne(filter: g => g.Id == item.Id, replacement: item);
            }
            catch
            {

                throw;
            }
        }

        // Delete
        public async Task<bool> DeleteItem(string id)
        {
            try
            {
                DeleteResult actionResult = await items.DeleteOneAsync(Builders<DbItem>.Filter.Eq("Id", id));
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch
            {

                throw;
            }
        }
    }
}
