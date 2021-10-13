namespace Brewlog.Api.DTOs
{
    public record UpdateFermentableDTO
    {
        public string Name { get; init; }
        public double WeightInGrams { get; init; }
    }
}