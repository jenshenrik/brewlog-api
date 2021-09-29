using System;
using System.Collections.Generic;
using System.Linq;
using Brewlog.Entities;

namespace Brewlog.Repositories
{
    public class InMemRecipeRepository : IRecipeRepository
    {
        private readonly List<Recipe> _recipes = new()
        {
            new Recipe 
            { 
                Id = Guid.NewGuid(), 
                Name = "Saison Automné", 
                OriginalGravity = 1.081M, 
                FinalGravity = 1.008M,
                IBU = 26,
                EBC = 20,
                Fermentables = new List<Fermentable>()
                {
                    new Fermentable { Id = Guid.NewGuid(), Name = "Maris otter", WeightInGrams = 4000 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Biscuit", WeightInGrams = 150 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Munich", WeightInGrams = 300 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Crystal malt", WeightInGrams = 100 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Demerara sugar", WeightInGrams = 300 },
                },
                HopAdditions = new List<HopAddition>()
                {
                    new HopAddition { Id = Guid.NewGuid(), Name = "Styrian Goldings", WeightInGrams = 28, MinutesInBoil = 60 },
                    new HopAddition { Id = Guid.NewGuid(), Name = "Saaz", WeightInGrams = 14, MinutesInBoil = 20 },
                },
                Yeast = "Wyeast 3724 Belgian Saison",
                CreatedDate = DateTimeOffset.UtcNow 
            },
            new Recipe 
            { 
                Id = Guid.NewGuid(), 
                Name = "Pam Grier", 
                OriginalGravity = 1.060M, 
                FinalGravity = 1.014M,
                IBU = 30,
                EBC = 46,
                Fermentables = new List<Fermentable>()
                {
                    new Fermentable { Id = Guid.NewGuid(), Name = "2-row", WeightInGrams = 2000 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Brown malt", WeightInGrams = 300 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Munich", WeightInGrams = 300 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Flaked oats", WeightInGrams = 300 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Crystal malt", WeightInGrams = 200 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Carafa Special II", WeightInGrams = 100 },
                },
                HopAdditions = new List<HopAddition>()
                {
                    new HopAddition { Id = Guid.NewGuid(), Name = "Nugget", WeightInGrams = 18, MinutesInBoil = 60 },
                    new HopAddition { Id = Guid.NewGuid(), Name = "Centennial", WeightInGrams = 18, MinutesInBoil = 5 },
                    new HopAddition { Id = Guid.NewGuid(), Name = "Amarillo", WeightInGrams = 18, MinutesInBoil = 0 },
                },
                Yeast = "Wyeast 1028 London Ale",
                CreatedDate = DateTimeOffset.UtcNow 
            },
            new Recipe 
            { 
                Id = Guid.NewGuid(), 
                Name = "Springtime in Amarillo", 
                OriginalGravity = 1.063M, 
                FinalGravity = 1.007M,
                IBU = 47,
                EBC = 13,
                Fermentables = new List<Fermentable>()
                {
                    new Fermentable { Id = Guid.NewGuid(), Name = "2-row", WeightInGrams = 2200 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Wheat", WeightInGrams = 820 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Aromatic", WeightInGrams = 275 },
                    new Fermentable { Id = Guid.NewGuid(), Name = "Table sugar", WeightInGrams = 225 },
                },
                HopAdditions = new List<HopAddition>()
                {
                    new HopAddition { Id = Guid.NewGuid(), Name = "Amarillo", WeightInGrams = 15, MinutesInBoil = 60 },
                    new HopAddition { Id = Guid.NewGuid(), Name = "Amarillo", WeightInGrams = 10, MinutesInBoil = 20 },
                    new HopAddition { Id = Guid.NewGuid(), Name = "Amarillo", WeightInGrams = 10, MinutesInBoil = 0 },
                },
                Yeast = "Wyeast 3711 French Saison",
                CreatedDate = DateTimeOffset.UtcNow 
            },
        };

        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipes;
        }

        public Recipe GetRecipe(Guid id)
        {
            return _recipes.FirstOrDefault(r => r.Id == id);
        }

        public void CreateRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var index = _recipes.FindIndex(existingRecipe => existingRecipe.Id == recipe.Id);
            _recipes[index] = recipe;
        }

        public void DeleteRecipe(Guid id)
        {
            var index = _recipes.FindIndex(existingRecipe => existingRecipe.Id == id);
            _recipes.RemoveAt(index);
        }
    }
}