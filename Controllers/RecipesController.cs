using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Brewlog.DTOs.Recipe;
using Brewlog.DTOs;
using Brewlog.Entities;
using Brewlog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Brewlog.Services;

namespace Brewlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeRepository recipeRepository, 
                                 IBatchRepository batchRepository,
                                 IRecipeService recipeService)
        {
            _recipeRepository = recipeRepository;
            _recipeService = recipeService;
            _batchRepository = batchRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeListElement>> GetRecipesAsync()
        {
            return await _recipeService.GetAllRecipesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipeAsync(Guid id)
        {
            var recipe = await _recipeService.GetRecipeAsync(id);
            if (recipe is null) return NotFound();

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> CreateRecipeAsync(CreateRecipeDTO recipeDto)
        {
            Recipe recipe = new()
            {
                Id = Guid.NewGuid(),
                Name = recipeDto.Name,
                Style = recipeDto.Style,
                OriginalGravity = recipeDto.OriginalGravity,
                FinalGravity = recipeDto.FinalGravity,
                IBU = recipeDto.IBU,
                EBC = recipeDto.EBC,
                Yeast = recipeDto.Yeast,
                Fermentables = recipeDto.Fermentables.Select(
                    f => new Fermentable { Id = Guid.NewGuid(), Name = f.Name, WeightInGrams = f.WeightInGrams, Percent = f.Percent }
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
                Style = recipeDto.Style,
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

        [HttpPut("{recipeId}/batch/{batchNumber}")]
        public async Task<ActionResult> UpdateBatchAsync(Guid recipeId, int batchNumber, UpdateBatchDTO batchDto)
        {
            var existingBatch = await _batchRepository.GetBatchAsync(recipeId, batchNumber);

            if (existingBatch is null)
            {
                return NotFound();
            }

            Batch updateBatch = existingBatch with {
                BoilGravity = batchDto.BoilGravity,
                BrewDate = batchDto.BrewDate,
                FinalGravity = batchDto.FinalGravity,
                Notes = batchDto.Notes,
                OriginalGravity = batchDto.OriginalGravity
            };

            await _batchRepository.UpdateBatchAsync(updateBatch);

            return NoContent();
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