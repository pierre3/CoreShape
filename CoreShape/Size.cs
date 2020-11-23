using System;

namespace CoreShape
{
    public readonly struct Size : IEquatable<Size>
    {
        public float Width { get; init; }
        public float Height { get; init; }
        public Size(float width, float height) => (Width, Height) = (width, height);
        public Size(Size p) => (Width, Height) = (p.Width, p.Height);

        public bool Equals(Size other) => (Width, Height) == (other.Width, other.Height);
        public override bool Equals(object? obj) => (obj is Size other) && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Width, Height);
        public static bool operator ==(Size left, Size right) => left.Equals(right);
        public static bool operator !=(Size left, Size right) => !(left == right);
        public override string ToString() => $"{nameof(Size)}({Width}, {Height})";
    }
}
