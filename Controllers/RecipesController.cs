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
        private readonly InMemRecipeRepository repository;

        public RecipesController()
        {
            repository = new InMemRecipeRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> Recipes()
        {
            var recipes = repository.GetRecipes();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipe(Guid id)
        {
            var recipe = repository.GetRecipe(id);
            if (recipe is null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
    }
}