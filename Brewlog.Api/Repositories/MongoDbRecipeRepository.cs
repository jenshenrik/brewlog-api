using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewlog.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Brewlog.Api.Repositories
{
    public class MongoDbRecipeRepository : IRecipeRepository
    {
        private const string databaseName = "brewlogdb";
        private const string collectionName = "recipes";

        private readonly IMongoCollection<Recipe> recipesCollection;
        private readonly FilterDefinitionBuilder<Recipe> filterBuilder = Builders<Recipe>.Filter;

        public MongoDbRecipeRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            recipesCollection = database.GetCollection<Recipe>(collectionName);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            return await recipesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Recipe> GetRecipeAsync(Guid id)
        {
            var filter = filterBuilder.Eq(r => r.Id, id);
            return await recipesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            await recipesCollection.InsertOneAsync(recipe);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var filter = filterBuilder.Eq(existingRecipe => existingRecipe.Id, recipe.Id);
            await recipesCollection.ReplaceOneAsync(filter, recipe);
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            var filter = filterBuilder.Eq(r => r.Id, id);
            await recipesCollection.DeleteOneAsync(filter);
        }
    }
}