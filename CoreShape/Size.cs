using System;
namespace CoreShape
{
    [ReadonlyStructGenerator.ReadonlyStruct]
    public partial struct Size
    {
        public float Width { get; init; }
        public float Height { get; init; }
    }
}
