using System.ComponentModel.DataAnnotations;

namespace Brewlog.DTOs
{
    public record CreateRecipeDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 2)]
        public decimal OriginalGravity { get; init; }
    }
}