using System;

namespace CoreShape
{
    [ReadonlyStructGenerator.ReadonlyStruct]
    public partial struct Point
    {
        public float X { get; init; }
        public float Y { get; init; }

    }
}
