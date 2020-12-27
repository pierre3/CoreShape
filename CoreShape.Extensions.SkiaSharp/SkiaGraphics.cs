using CoreShape.Graphics;
using SkiaSharp;

namespace CoreShape.Extensions.SkiaSharp
{
    public class SkiaGraphics : IGraphics
    {
        protected virtual SKCanvas Canvas { get; set; }

        public SkiaGraphics(SKCanvas canvas)
        {
            Canvas = canvas;
        }
        public virtual void ClearCanvas(Color color)
        {
            Canvas.Clear(color.ToSk());
        }

        public virtual void DrawRectangle(Rectangle rectangle, Stroke stroke)
        {
            using var paint = new SKPaint().SetStroke(stroke);
            Canvas.DrawRect(rectangle.ToSk(), paint);
        }

        public virtual void FillRectangle(Rectangle rectangle, Fill fill)
        {
            using var paint = new SKPaint().SetFill(fill);
            Canvas.DrawRect(rectangle.ToSk(), paint);
        }

        public virtual void DrawOval(Rectangle rectangle, Stroke stroke)
        {
            using var paint = new SKPaint().SetStroke(stroke);
            Canvas.DrawOval(rectangle.ToSk(), paint );
        }

        public virtual void FillOval(Rectangle rectangle, Fill fill)
        {
            using var paint = new SKPaint().SetFill(fill);
            Canvas.DrawOval(rectangle.ToSk(), paint);
        }
    }
}
