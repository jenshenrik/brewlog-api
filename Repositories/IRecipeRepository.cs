using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewlog.Entities;

namespace Brewlog.Repositories
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task<Recipe> GetRecipeAsync(Guid id);
        Task CreateRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Guid id);
    }
}