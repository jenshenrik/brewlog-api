using System;

namespace Brewlog.DTOs.Recipe
{
    public record RecipeListElement
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Style { get; init; }
        public decimal ABV { get; init; }
    }
}