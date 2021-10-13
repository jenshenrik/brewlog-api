using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Brewlog.Api.DTOs;
using Brewlog.Api.Entities;
using Brewlog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Brewlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeDTO>> GetRecipesAsync()
        {
            var recipes = (await _recipeRepository.GetRecipesAsync())
                .Select(recipe => recipe.AsDTO());
            return recipes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipeAsync(Guid id)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(id);
            if (recipe is null)
            {
                return NotFound();
            }
            return Ok(recipe.AsDTO());
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> CreateRecipeAsync(CreateRecipeDTO recipeDto)
        {
            Recipe recipe = new()
            {
                Id = Guid.NewGuid(),
                Name = recipeDto.Name,
                OriginalGravity = recipeDto.OriginalGravity,
                FinalGravity = recipeDto.FinalGravity,
                IBU = recipeDto.IBU,
                EBC = recipeDto.EBC,
                Yeast = recipeDto.Yeast,
                Fermentables = recipeDto.Fermentables.Select(
                    f => new Fermentable { Id = Guid.NewGuid(), Name = f.Name, WeightInGrams = f.WeightInGrams }
                ),
                HopAdditions = recipeDto.HopAdditions.Select(
                    h => new HopAddition { Id = Guid.NewGuid(), Name = h.Name, WeightInGrams = h.WeightInGrams, MinutesInBoil = h.MinutesInBoil }
                ),
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _recipeRepository.CreateRecipeAsync(recipe);

            return CreatedAtAction(nameof(GetRecipeAsync), new { id = recipe.Id }, recipe.AsDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecipeAsync(Guid id, UpdateRecipeDTO recipeDto)
        {
            var existingRecipe = await _recipeRepository.GetRecipeAsync(id);

            if (existingRecipe is null)
            {
                return NotFound();
            }

            Recipe updateRecipe = existingRecipe with {
                Name = recipeDto.Name,
                OriginalGravity = recipeDto.OriginalGravity,
                FinalGravity = recipeDto.FinalGravity,
                IBU = recipeDto.IBU,
                EBC = recipeDto.EBC,
                Yeast = recipeDto.Yeast,
                Fermentables = recipeDto.Fermentables.Select(
                    f => new Fermentable { Id = Guid.NewGuid(), Name = f.Name, WeightInGrams = f.WeightInGrams }
                ),
                HopAdditions = recipeDto.HopAdditions.Select(
                    h => new HopAddition { Id = Guid.NewGuid(), Name = h.Name, WeightInGrams = h.WeightInGrams, MinutesInBoil = h.MinutesInBoil }
                ),
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _recipeRepository.UpdateRecipeAsync(updateRecipe);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(id);

            if (recipe is null)
            {
                return NotFound();
            }

            await _recipeRepository.DeleteRecipeAsync(id);
            return NoContent();
        }

    }
}