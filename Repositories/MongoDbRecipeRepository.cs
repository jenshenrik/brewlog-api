using System;
using System.Collections.Generic;
using Brewlog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Brewlog.Repositories
{
    public class MongoDbRecipeRepository : IRecipeRepository
    {
        private const string databaseName = "brewlog";
        private const string collectionName = "recipes";
        public readonly IMongoCollection<Recipe> recipesCollection;
        public readonly FilterDefinitionBuilder<Recipe> filterBuilder = Builders<Recipe>.Filter;
        public MongoDbRecipeRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            recipesCollection = database.GetCollection<Recipe>(collectionName);
        }

        public void CreateRecipe(Recipe recipe)
        {
            recipesCollection.InsertOne(recipe);
        }

        public void DeleteRecipe(Guid id)
        {
            var filter = filterBuilder.Eq(r => r.Id, id);
            recipesCollection.DeleteOne(filter);
        }

        public Recipe GetRecipe(Guid id)
        {
            var filter = filterBuilder.Eq(r => r.Id, id);
            return recipesCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return recipesCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var filter = filterBuilder.Eq(existingRecipe => existingRecipe.Id, recipe.Id);
            recipesCollection.ReplaceOne(filter, recipe);
        }
    }
}