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
                CreatedDate = DateTimeOffset.UtcNow
            };

            _recipeRepository.CreateRecipe(recipe);

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe.AsDTO());
        }
    }
}