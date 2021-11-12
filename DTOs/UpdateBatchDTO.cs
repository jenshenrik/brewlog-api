using System;

namespace Brewlog.DTOs
{
    public record UpdateBatchDTO
    {
        public string Notes { get; init; }
        public decimal? OriginalGravity { get; init; }
        public decimal? BoilGravity { get; init; }
        public decimal? FinalGravity { get; init; }
        public DateTimeOffset BrewDate { get; init; }
    }
}