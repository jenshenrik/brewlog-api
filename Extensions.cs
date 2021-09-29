using System.Linq;
using Brewlog.Entities;
using Brewlog.DTOs;

namespace Brewlog
{
    public static class Extensions
    {
        public static RecipeDTO AsDTO(this Recipe recipe)
        {
            return new RecipeDTO 
            { 
                Id = recipe.Id, 
                Name = recipe.Name, 
                OriginalGravity = recipe.OriginalGravity, 
                FinalGravity = recipe.FinalGravity,
                IBU = recipe.IBU,
                EBC = recipe.EBC,
                Yeast = recipe.Yeast,
                Fermentables = recipe.Fermentables.Select(f => f.AsDTO()),
                HopAdditions = recipe.HopAdditions.Select(h => h.AsDTO()),
                CreatedDate = recipe.CreatedDate 
            };
        }

        public static FermentableDTO AsDTO(this Fermentable fermentable)
        {
            return new FermentableDTO { Id = fermentable.Id, Name = fermentable.Name, WeightInGrams = fermentable.WeightInGrams};
        }

        public static HopAdditionDTO AsDTO(this HopAddition hops)
        {
            return new HopAdditionDTO { Id = hops.Id, Name = hops.Name, WeightInGrams = hops.WeightInGrams, MinutesInBoil = hops.MinutesInBoil };
        }
    }
}