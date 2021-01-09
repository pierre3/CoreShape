using System;

namespace CoreShape.Shapes
{
    public class ResizeHandleSW : ResizeHandle
    {
        public ResizeHandleSW(Rectangle bounds) : base(bounds)
        {
            Type = ResizeType.ResizeSW;
        }

        public override Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return new Rectangle(p.X, parentBounds.Top, parentBounds.Right - p.X, p.Y - parentBounds.Top);
        }

        public override void SetLocation(Rectangle parentBounds)
        {
            var center = new Point(parentBounds.Left, parentBounds.Bottom);
            MoveTo(center);
        }
    }
}
