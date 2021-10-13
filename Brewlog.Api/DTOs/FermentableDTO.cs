using System;

namespace Brewlog.Api.DTOs
{
    public record FermentableDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double WeightInGrams { get; init; }
    }
}