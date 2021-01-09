using System;

namespace CoreShape.Shapes
{
    public class ResizeHandleNE : ResizeHandle
    {
        public ResizeHandleNE(Rectangle bounds) : base(bounds)
        {
            Type = ResizeType.ResizeNE;
        }

        public override Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return new Rectangle(parentBounds.Left, p.Y, p.X - parentBounds.Left, parentBounds.Bottom - p.Y);
        }

        public override void SetLocation(Rectangle parentBounds)
        {
            var center = new Point(parentBounds.Right, parentBounds.Top);
            MoveTo(center);
        }

        
    }
}
