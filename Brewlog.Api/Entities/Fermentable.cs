using System;

namespace Brewlog.Api.Entities
{
    public record Fermentable
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double WeightInGrams { get; init; }
    }
}