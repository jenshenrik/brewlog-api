using System;
using Brewlog.Entities;

namespace Brewlog.DTOs
{
    public record HopAdditionDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double? WeightInGrams { get; init; }
        public double? IBUs { get; init; }
        public HopAdditionType Type { get; init; }
        public int MinutesInBoil { get; init; }
        public int Duration { get; init; }
    }
}