using System;
using MongoDB.Bson;

namespace Brewlog.Entities
{
    public record Batch
    {
        public ObjectId Id { get; set ;}
        public int Number { get; init; }
        public Guid RecipeId { get; init; }
        public string Notes { get; init; }
        public decimal? OriginalGravity { get; init; }
        public decimal? BoilGravity { get; init; }
        public decimal? FinalGravity { get; init; }
        public DateTimeOffset BrewDate { get; init; }
    }
}