using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IBatchRepository _batchRepository;

        public RecipesController(IRecipeRepository recipeRepository, 
                                 IBatchRepository batchRepository)
        {
            _recipeRepository = recipeRepository;
            _batchRepository = batchRepository;
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
            if (recipe is null) return NotFound();

            var dto =  recipe.AsDTO() with 
            { 
                Batches = (IEnumerable<BatchDTO>)(await _batchRepository
                    .GetBatchesForRecipeAsync(id))
                    .Select(b => b.AsDTO())
            };
            return Ok(dto);
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
                CreatedDate = DateTimeOffset.UtcNow,
                WaterProfile = recipeDto.WaterProfile?.FromDTO(),
                MashPh = recipeDto.MashPh
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
                WaterProfile = recipeDto.WaterProfile?.FromDTO(),
                CreatedDate = DateTimeOffset.UtcNow,
                MashPh = recipeDto.MashPh
            };

            await _recipeRepository.UpdateRecipeAsync(updateRecipe);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(id);
            if (recipe is null)  return NotFound();

            await _recipeRepository.DeleteRecipeAsync(id);
            return NoContent();
        }

        [HttpGet("{recipeId}/batch/{batchNumber}")]
        public async Task<ActionResult<Batch>> GetBatchAsync(Guid recipeId, int batchNumber)
        {
            var batch = await _batchRepository.GetBatchAsync(recipeId, batchNumber);
            if (batch is null)
            {
                return NoContent();
            }

            return Ok(batch);
        }

        [HttpPost("{recipeId}/batch")]
        public async Task<ActionResult<BatchDTO>> CreateBatchAsync(Guid recipeId, CreateBatchDTO batchDto)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(recipeId);
            if (recipe is null) return NotFound();
            
            Batch batch = new()
            {
                RecipeId = recipeId,
                Number = await _batchRepository.GetNextBatchNumber(recipeId),
                Notes = batchDto.Notes,
                BoilGravity = batchDto.BoilGravity,
                FinalGravity = batchDto.FinalGravity,
                OriginalGravity = batchDto.OriginalGravity,
                BrewDate = batchDto.BrewDate
            };

            BatchDTO createdDto = new()
            {
                RecipeId = batch.RecipeId,
                Number = batch.Number,
                BoilGravity = batch.BoilGravity,
                OriginalGravity = batch.OriginalGravity,
                FinalGravity = batch.FinalGravity,
                Notes = batch.Notes,
                BrewDate = batch.BrewDate
            };
            
            await _batchRepository.CreateBatchAsync(batch);
            return CreatedAtAction(nameof(GetBatchAsync), new { recipeId = batch.RecipeId, batchNumber = batch.Number}, createdDto);
        }

        
        [HttpDelete("{recipeId}/batch/{batchNumber}")]
        public async Task<ActionResult> DeleteBatchAsync(Guid recipeId, int batchNumber)
        {
            var batch = await _batchRepository.GetBatchAsync(recipeId, batchNumber);
            if (batch is null) return NotFound();

            await _batchRepository.DeleteBatchAsync(recipeId, batchNumber);
            return NoContent();
        }
    }
}