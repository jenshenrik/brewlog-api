using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brewlog.Entities;

namespace Brewlog.Repositories
{
    public interface IBatchRepository
    {
        Task<IEnumerable<Batch>> GetBatchesForRecipeAsync(Guid recipeId);
        Task<Batch> GetBatchAsync(Guid recipeId, int batchNumber);
        Task CreateBatchAsync(Batch batch);
        Task UpdateBatchAsync(Batch batch);
        Task DeleteBatchAsync(Guid recipeId, int batchNumber);
        Task<int> GetNextBatchNumber(Guid recipeId);
    }
}