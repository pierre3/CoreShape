using System;

namespace CoreShape
{
    public readonly struct Point : IEquatable<Point>
    {
        public float X { get; init; }
        public float Y { get; init; }
        public Point(float x, float y) => (X, Y) = (x, y);
        public Point(Point p) => (X, Y) = (p.X, p.Y);

        public bool Equals(Point other) => (X, Y) == (other.X, other.Y);
        public override bool Equals(object? obj) => (obj is Point other) && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(Point left, Point right) => left.Equals(right);
        public static bool operator !=(Point left, Point right) => !(left == right);
        public override string ToString() => $"{nameof(Point)}({X}, {Y})";
    }
}
