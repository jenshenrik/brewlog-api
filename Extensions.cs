using System.Linq;
using Brewlog.Entities;
using Brewlog.DTOs;
using Brewlog.DTOs.Recipe;

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
                CreatedDate = recipe.CreatedDate ,
                WaterProfile = recipe.WaterProfile?.AsDTO(),
                MashPh = recipe.MashPh
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

        public static WaterProfileDTO AsDTO(this WaterProfile water)
        {
            return new WaterProfileDTO
            {
                Ca = water.Ca,
                Mg = water.Mg,
                Na = water.Na,
                Cl = water.Cl,
                SO4 = water.SO4,
                HCO3 = water.HCO3
            };
        }

        public static WaterProfile FromDTO(this WaterProfileDTO water)
        {
            return new WaterProfile
            {
                Ca = water.Ca,
                Mg = water.Mg,
                Na = water.Na,
                Cl = water.Cl,
                SO4 = water.SO4,
                HCO3 = water.HCO3
            };
        }

        public static BatchDTO AsDTO(this Batch b)
        {
            return new BatchDTO
            {
                BoilGravity = b.BoilGravity,
                BrewDate = b.BrewDate,
                FinalGravity = b.FinalGravity,
                Notes = b.Notes,
                Number = b.Number,
                OriginalGravity = b.OriginalGravity,
                RecipeId = b.RecipeId
            };
        }
    }
}