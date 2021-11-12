using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewlog.Entities;
using MongoDB.Driver;

namespace Brewlog.Repositories
{
    public class MongoDbBatchRepository : IBatchRepository
    {
        private const string databaseName = "brewlogdb";
        private const string collectionName = "batches";

        private readonly IMongoCollection<Batch> batchesCollection;
        private readonly FilterDefinitionBuilder<Batch> filterBuilder = Builders<Batch>.Filter;

        public MongoDbBatchRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            batchesCollection = database.GetCollection<Batch>(collectionName);
        }

        public async Task CreateBatchAsync(Batch batch)
        {
            await batchesCollection.InsertOneAsync(batch);
        }

        public async Task DeleteBatchAsync(Guid recipeId, int batchNumber)
        {
            var filter = filterBuilder.Eq(b => b.RecipeId, recipeId) 
                & filterBuilder.Eq(b => b.Number, batchNumber);
            await batchesCollection.DeleteOneAsync(filter);
        }

        public async Task<Batch> GetBatchAsync(Guid recipeId, int batchNumber)
        {
            var filter = filterBuilder.Eq(b => b.RecipeId, recipeId) 
                & filterBuilder.Eq(b => b.Number, batchNumber);
            return await batchesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Batch>> GetBatchesForRecipeAsync(Guid recipeId)
        {
            var filter = filterBuilder.Eq(b => b.RecipeId, recipeId);
            return await batchesCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateBatchAsync(Batch batch)
        {
            var filter = filterBuilder.Eq(existingBatch => existingBatch.RecipeId, batch.RecipeId) 
                & filterBuilder.Eq(existingBatch => existingBatch.Number, batch.Number);
            await batchesCollection.ReplaceOneAsync(filter, batch);
        }

        public async Task<int> GetNextBatchNumber(Guid recipeId)
        {
            var batches = await GetBatchesForRecipeAsync(recipeId);
            var latestBatch = batches.Max(b => b.Number);
            return await Task.FromResult(latestBatch + 1);
        }
    }
}