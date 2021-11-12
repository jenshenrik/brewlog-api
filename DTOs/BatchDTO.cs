using System;

namespace Brewlog.DTOs
{
    public record BatchDTO
    {
        public Guid RecipeId { get; init; }
        public int Number { get; init; }
        public string Notes { get; init; }
        public decimal? OriginalGravity { get; init; }
        public decimal? BoilGravity { get; init; }
        public decimal? FinalGravity { get; init; }
        public DateTimeOffset BrewDate { get; init; }
    }
}