using System;

namespace Brewlog.DTOs
{
    public record FermentableDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double Percent { get; init; }
        public double WeightInGrams { get; init; }
    }
}