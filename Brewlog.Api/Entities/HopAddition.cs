using System;

namespace Brewlog.Api.Entities
{
    public record HopAddition
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double WeightInGrams { get; init; }
        public int MinutesInBoil { get; init; }
    }
}