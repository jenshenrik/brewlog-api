using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewlog.DTOs.Recipe;

namespace Brewlog.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeListElement>> GetAllRecipesAsync();
        Task<RecipeDTO> GetRecipeAsync(Guid recipeId);
    }
}