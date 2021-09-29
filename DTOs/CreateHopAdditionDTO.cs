namespace Brewlog.DTOs
{
    public record CreateHopAdditionDTO
    {
        public string Name { get; init; }
        public double WeightInGrams { get; init; }
        public int MinutesInBoil { get; init; }
    }
}