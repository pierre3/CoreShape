using System;

namespace CoreShape
{
    public readonly struct Rectangle : IEquatable<Rectangle>
    {
        public Point Location { get; init; }
        public Size Size { get; init; }

        public float Left  => Location.X;
        public float Top => Location.Y;
        public float Right { get; }
        public float Bottom { get; }
            

        public Rectangle(Point location, Size size)
        {
            Location = location;
            Size = size;
            Right = Location.X + Size.Width;
            Bottom = Location.Y + Size.Height;
        }

        public Rectangle(float x, float y, float width, float height)
            :this(new Point(x,y), new Size(width,height))
        {
        }
        public Rectangle(Rectangle rect) : this(rect.Location, rect.Size) { }


        public bool Equals(Rectangle other) => Location == other.Location && Size == other.Size;

        public override bool Equals(object? obj) => (obj is Rectangle other) && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Location, Size);

        public static bool operator ==(Rectangle left, Rectangle right) => left.Equals(right);

        public static bool operator !=(Rectangle left, Rectangle right) => !(left == right);

        public override string ToString() => $"{nameof(Rectangle)}({Location}, {Size})";
    }
}
