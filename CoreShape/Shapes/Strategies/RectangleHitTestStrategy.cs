using System;

namespace CoreShape.Shapes
{

    public class RectangleHitTestStrategy : IHitTestStrategy<RectangleShape>
    {
        public bool HitTest(Point p, RectangleShape shape)
        {
            if (shape.Stroke is not null)
            {
                if (p.X >= shape.Bounds.Left && p.X <= shape.Bounds.Right
                    && p.Y >= shape.Bounds.Top - 2 && p.Y <= shape.Bounds.Top + 2)
                {
                    return true;
                }
                if (p.X >= shape.Bounds.Left && p.X <= shape.Bounds.Right
                    && p.Y >= shape.Bounds.Bottom - 2 && p.Y <= shape.Bounds.Bottom + 2)
                {
                    return true;
                }
                if (p.Y >= shape.Bounds.Top && p.Y <= shape.Bounds.Bottom
                    && p.X >= shape.Bounds.Left - 2 && p.X <= shape.Bounds.Left + 2)
                {
                    return true;
                }
                if (p.Y >= shape.Bounds.Top && p.Y <= shape.Bounds.Bottom
                    && p.X >= shape.Bounds.Right - 2 && p.X <= shape.Bounds.Right + 2)
                {
                    return true;
                }
            }
            if (shape.Fill is not null)
            {
                if (shape.Bounds.Left <= p.X && p.X <= shape.Bounds.Right
                    && shape.Bounds.Top <= p.Y && p.Y <= shape.Bounds.Bottom)
                {
                    return true;
                }
            }

            return false;
        }
    }

}
