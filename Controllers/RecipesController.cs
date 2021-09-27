using System;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<Recipe>> Recipes()
        {
            var recipes = _recipeRepository.GetRecipes();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipe(Guid id)
        {
            var recipe = _recipeRepository.GetRecipe(id);
            if (recipe is null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
    }
}