using System;

namespace CoreShape
{
    [ReadonlyStructGenerator.ReadonlyStruct]
    public partial struct Rectangle
    {
        public Point Location { get; init; }
        public Size Size { get; init; }

        public float Left  => Location.X;
        public float Top => Location.Y;
        public float Right { get; }
        public float Bottom { get; }
        public float Width => Size.Width;
        public float Height => Size.Height;

        public Rectangle(Point location,Size size)
        {
            Location = location;
            Size = size;
            Right = location.X + size.Width;
            Bottom = location.Y + size.Height;
        }

        public Rectangle(float left, float top, float width, float height)
            : this(new Point(left, top), new Size(width, height)) { }

    }
}
