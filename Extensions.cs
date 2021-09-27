using Brewlog.Entities;
using Brewlog.DTOs;

namespace Brewlog
{
    public static class Extensions
    {
        public static RecipeDTO AsDTO(this Recipe recipe)
        {
            return new RecipeDTO { Id = recipe.Id, Name = recipe.Name, OriginalGravity = recipe.OriginalGravity, CreatedDate = recipe.CreatedDate };
        }
    }
}