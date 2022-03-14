using SkiaSharp;

namespace CoreShape.Extensions.SkiaSharp
{
    public static class TypeConvertExtensions
    {
        public static SKPoint ToSk(this Point p) => new(p.X, p.Y);
        public static SKSize ToSk(this Size s) => new(s.Width, s.Height);
        public static SKRect ToSk(this Rectangle rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
        public static SKColor ToSk(this Color color) => new(color.R, color.G, color.B, color.A);
    }
}
