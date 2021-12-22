using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brewlog.DTOs.Recipe;
using Brewlog.Repositories;

namespace Brewlog.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<RecipeListElement>> GetAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetRecipesAsync();
            return recipes.Select(r => new RecipeListElement
            {
                Id = r.Id,
                Name = r.Name,
                Style = "To be implemented",
                ABV = r.ABV
            });
        }

        public async Task<RecipeDTO> GetRecipeAsync(Guid recipeId)
        {
            return (await _recipeRepository.GetRecipeAsync(recipeId)).AsDTO();
        }
    }
}