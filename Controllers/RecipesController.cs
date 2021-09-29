using System;
using System.Collections.Generic;
using System.Linq;
using Brewlog.DTOs;
using Brewlog.Entities;
using Brewlog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Brewlog.Controllers
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
        public ActionResult<IEnumerable<RecipeDTO>> Recipes()
        {
            var recipes = _recipeRepository.GetRecipes().Select(recipe => recipe.AsDTO());
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDTO> GetRecipe(Guid id)
        {
            var recipe = _recipeRepository.GetRecipe(id);
            if (recipe is null)
            {
                return NotFound();
            }
            return Ok(recipe.AsDTO());
        }

        [HttpPost]
        public ActionResult<RecipeDTO> CreateRecipe(CreateRecipeDTO recipeDto)
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

            _recipeRepository.CreateRecipe(recipe);

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe.AsDTO());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRecipe(Guid id, UpdateRecipeDTO recipeDto)
        {
            var existingRecipe = _recipeRepository.GetRecipe(id);

            if (existingRecipe is null)
            {
                return NotFound();
            }

            Recipe updateRecipe = existingRecipe with {
                Name = recipeDto.Name,
                OriginalGravity = recipeDto.OriginalGravity
            };

            _recipeRepository.UpdateRecipe(updateRecipe);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRecipe(Guid id)
        {
            var recipe = _recipeRepository.GetRecipe(id);

            if (recipe is null)
            {
                return NotFound();
            }

            _recipeRepository.DeleteRecipe(id);
            return NoContent();
        }
    }
}