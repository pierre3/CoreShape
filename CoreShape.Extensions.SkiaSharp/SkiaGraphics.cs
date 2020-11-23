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
            Canvas.DrawRect(rectangle.ToSk(), new SKPaint().SetStroke(stroke));
        }

        public virtual void FillRectangle(Rectangle rectangle, Fill fill)
        {
            Canvas.DrawRect(rectangle.ToSk(), new SKPaint().SetFill(fill));
        }

    }
}
