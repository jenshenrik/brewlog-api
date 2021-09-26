using System;

namespace Brewlog.Entities
{
    public record Recipe
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal OriginalGravity { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}