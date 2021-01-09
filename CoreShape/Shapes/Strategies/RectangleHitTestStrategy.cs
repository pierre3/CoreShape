using System;

namespace CoreShape.Shapes
{

    public class RectangleHitTestStrategy : IHitTestStrategy<RectangleShape>
    {
        public bool HitTest(Point p, RectangleShape shape)
        {
            var bounds = (shape.Bounds.Size.Width < 0 || shape.Bounds.Size.Height < 0)
            ? new Rectangle(
                Math.Min(shape.Bounds.Left, shape.Bounds.Right),
                Math.Min(shape.Bounds.Top, shape.Bounds.Bottom),
                Math.Abs(shape.Bounds.Size.Width),
                Math.Abs(shape.Bounds.Size.Height))
            : shape.Bounds;

            if (shape.Stroke is not null)
            {
                if (p.X >= bounds.Left && p.X <= bounds.Right
                    && p.Y >= bounds.Top - 2 && p.Y <= bounds.Top + 2)
                {
                    return true;
                }
                if (p.X >= bounds.Left && p.X <= bounds.Right
                    && p.Y >= bounds.Bottom - 2 && p.Y <= bounds.Bottom + 2)
                {
                    return true;
                }
                if (p.Y >= bounds.Top && p.Y <= bounds.Bottom
                    && p.X >= bounds.Left - 2 && p.X <= bounds.Left + 2)
                {
                    return true;
                }
                if (p.Y >= bounds.Top && p.Y <= bounds.Bottom
                    && p.X >= bounds.Right - 2 && p.X <= bounds.Right + 2)
                {
                    return true;
                }
            }
            if (shape.Fill is not null)
            {
                if (bounds.Left <= p.X && p.X <= bounds.Right
                    && bounds.Top <= p.Y && p.Y <= bounds.Bottom)
                {
                    return true;
                }
            }

            return false;
        }
    }

}
