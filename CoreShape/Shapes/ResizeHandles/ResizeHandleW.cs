using System;

namespace CoreShape.Shapes
{
    public class ResizeHandleW : ResizeHandleBase
    {
        public ResizeHandleW(Rectangle bounds) : base(bounds)
        {
            HitResult = HitResult.ResizeW;
        }

        public override Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return new Rectangle(p.X, parentBounds.Top, parentBounds.Right - p.X, parentBounds.Size.Height);
        }

        public override void SetLocation(Rectangle parentBounds)
        {
            var center = new Point(parentBounds.Left, parentBounds.Top + parentBounds.Size.Height / 2);
            MoveTo(center);
        }
    }
}
