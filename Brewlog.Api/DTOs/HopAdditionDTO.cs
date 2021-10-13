using System;

namespace Brewlog.Api.DTOs
{
    public record HopAdditionDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double WeightInGrams { get; init; }
        public int MinutesInBoil { get; init; }
    }
}