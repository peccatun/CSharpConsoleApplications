using System;

namespace ChaningExample.Attributes
{
    public sealed class MapFromAttribute : Attribute
    {
        public MapFromAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
