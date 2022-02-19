namespace Brewlog.DTOs
{
    public record CreateFermentableDTO
    {
        public string Name { get; init; }
        public double Percent { get; init; }
        public double WeightInGrams { get; init; }
    }
}