using System;
using Brewlog.Entities;

namespace Brewlog.DTOs
{
    public record RecipeDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal OriginalGravity { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}