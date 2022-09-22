using ChaningExample.Attributes;
using System;

namespace ChaningExample.Entities
{
    [MapFrom("Motorcycles")]
    public sealed class Entity : BaseEntity
    {
        [MapFrom("Make")]
        public string Make { get; set; }

        [MapFrom("Model")]
        public string Model { get; set; }

        [MapFrom("ProductionDate")]
        public DateTime ProductionDate { get; set; }

        [MapFrom("StartKilometers")]
        public int StartKilometers { get; set; }

        [MapFrom("ApplicationUserId")]
        public int ApplicationUserId { get; set; }
    }
}
