using System;

namespace CoreShape.Shapes
{
    public class ResizeHandleN : ResizeHandle
    {
        public ResizeHandleN(Rectangle bounds) : base(bounds)
        { 
            Type = ResizeType.ResizeN;
        }

        public override Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return new Rectangle(parentBounds.Left, p.Y, parentBounds.Size.Width, parentBounds.Bottom - p.Y);
        }

        public override void SetLocation(Rectangle parentBounds)
        {
            var center = new Point(parentBounds.Left + parentBounds.Size.Width / 2, parentBounds.Top);
            MoveTo(center);
        }
    }
}
