using System;

namespace CoreShape.Shapes
{
    public class ResizeHandleS : ResizeHandleBase
    {
        public ResizeHandleS(Rectangle bounds) : base(bounds)
        {
            HitResult = HitResult.ResizeS;
        }

        public override Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return new Rectangle(parentBounds.Left, parentBounds.Top, parentBounds.Size.Width, p.Y - parentBounds.Top);
        }

        public override void SetLocation(Rectangle parentBounds)
        {
            var center = new Point(parentBounds.Left + parentBounds.Size.Width/2, parentBounds.Bottom);
            MoveTo(center);
        }
    }
}
