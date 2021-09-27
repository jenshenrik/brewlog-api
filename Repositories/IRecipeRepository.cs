using System;
using System.Collections.Generic;
using Brewlog.Entities;

namespace Brewlog.Repositories
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetRecipes();
        Recipe GetRecipe(Guid id);
        void CreateRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Guid id);
    }
}