namespace Brewlog.DTOs
{
    public record WaterProfileDTO
    {
        public decimal Ca { get; init; }
        public decimal Mg { get; init; }
        public decimal Na { get; init; }
        public decimal Cl { get; init; }
        public decimal SO4 { get; init; }
        public decimal HCO3 { get; init; }
    }
}