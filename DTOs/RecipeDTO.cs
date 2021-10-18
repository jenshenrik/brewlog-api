using System;
using System.Collections.Generic;

namespace Brewlog.DTOs
{
    public record RecipeDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal OriginalGravity { get; init; }
        public decimal FinalGravity { get; init; }
        public decimal ABV { get; init; }
        public int EBC { get; init; }
        public int IBU { get; init; }
        public IEnumerable<FermentableDTO> Fermentables { get; init; }
        public IEnumerable<HopAdditionDTO> HopAdditions { get; init; }
        public string Yeast { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public WaterProfileDTO WaterProfile { get; init; }
        public decimal MashPh { get; init; }
    }
}