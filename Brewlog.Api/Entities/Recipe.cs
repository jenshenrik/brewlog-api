using System;
using System.Collections.Generic;

namespace Brewlog.Api.Entities
{
    public record Recipe
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal OriginalGravity { get; init; }
        public decimal FinalGravity { get; init; }
        public decimal ABV { get; init; }
        public int EBC { get; init; }
        public int IBU { get; init; }
        public IEnumerable<Fermentable> Fermentables { get; init; }
        public IEnumerable<HopAddition> HopAdditions { get; init; }
        public string Yeast { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}