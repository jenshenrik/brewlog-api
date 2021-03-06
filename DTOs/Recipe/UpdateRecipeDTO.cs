using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brewlog.DTOs.Recipe
{
    public record UpdateRecipeDTO
    {
        [Required]
        public string Name { get; init; }
        public string Style { get; init; }
        [Required]
        [Range(1, 2)]
        public decimal OriginalGravity { get; init; }
        [Required]
        [Range(1, 2)]
        public decimal FinalGravity { get; init; }
        public int EBC { get; init; }
        public int IBU { get; init; }
        [Required]
        public string Yeast { get; init; } 
        public IEnumerable<UpdateFermentableDTO> Fermentables { get; init; }
        public IEnumerable<UpdateHopAdditionDTO> HopAdditions { get; init; }
        public WaterProfileDTO WaterProfile { get; init; }
        public decimal MashPh { get; init; }
    }
}