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
            new Recipe { Id = Guid.NewGuid(), Name = "Saison Automne", OriginalGravity = 1.081M, CreatedDate = DateTimeOffset.UtcNow },
            new Recipe { Id = Guid.NewGuid(), Name = "Pam Grier", OriginalGravity = 1.060M, CreatedDate = DateTimeOffset.UtcNow },
            new Recipe { Id = Guid.NewGuid(), Name = "Springtime in Amarillo", OriginalGravity = 1.063M, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IEnumerable<Recipe> GetRecipes()
        {
            return _recipes;
        }

        public Recipe GetRecipe(Guid id)
        {
            return _recipes.FirstOrDefault(r => r.Id == id);
        }
    }
}