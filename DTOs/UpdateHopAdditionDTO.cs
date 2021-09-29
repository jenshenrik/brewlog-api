namespace Brewlog.DTOs
{
    public record UpdateHopAdditionDTO
    {
        public string Name { get; init; }
        public double WeightInGrams { get; init; }
        public int MinutesInBoil { get; init; }
    }
}