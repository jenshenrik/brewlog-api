using Brewlog.Entities;

namespace Brewlog.DTOs
{
    public record CreateHopAdditionDTO
    {
        public string Name { get; init; }
        public double? WeightInGrams { get; init; }
        public double? IBUs { get; init; }
        public int MinutesInBoil { get; init; }
        public int Duration { get; init; }
        public HopAdditionType Type { get; init; }
    }
}