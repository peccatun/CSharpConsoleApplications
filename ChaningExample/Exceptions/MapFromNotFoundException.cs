using System;

namespace ChaningExample.Exceptions
{
    public sealed class MapFromNotFoundException : Exception
    {
        public MapFromNotFoundException()
        {

        }

        public MapFromNotFoundException(string message) : base(message)
        {

        }
    }
}
