using System;

namespace CoreShape.Shapes
{
    public class ResizeHandleSE : ResizeHandle
    {
        public ResizeHandleSE(Rectangle bounds) : base(bounds)
        {
            Type = ResizeType.ResizeSE;
        }

        public override Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return new Rectangle(parentBounds.Left, parentBounds.Top, p.X - parentBounds.Left, p.Y - parentBounds.Top);
        }

        public override void SetLocation(Rectangle parentBounds)
        {
            var center = new Point(parentBounds.Right, parentBounds.Bottom);
            MoveTo(center);
        }
    }
}
