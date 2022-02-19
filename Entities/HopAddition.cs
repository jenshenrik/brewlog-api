using System;

namespace Brewlog.Entities
{
    public record HopAddition
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double? WeightInGrams { get; init; }
        public double? IBUs { get; init; }
        public int Duration { get; init; }
        public int MinutesInBoil { get; init; }
        public HopAdditionType Type { get; init; }
    }
}