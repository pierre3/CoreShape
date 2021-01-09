namespace CoreShape.Shapes
{
    public class OvalHitTestStrategy : IHitTestStrategy<RectangleShape>
    {
        public bool HitTest(Point p, RectangleShape shape)
        {
            static double Discriminant(float x, float y, float xr, float yr) => (x * x) / (xr * xr) + (y * y) / (yr * yr);
            var xr = shape.Bounds.Size.Width / 2;
            var yr = shape.Bounds.Size.Height / 2;
            var x = p.X - shape.Bounds.Left - xr;
            var y = p.Y - shape.Bounds.Top - yr;
            if (shape.Stroke is not null)
            {
                if (Discriminant(x, y, xr + 2, yr + 2) <= 1
                    && Discriminant(x, y, xr - 2, yr - 2) >= 1)
                {
                    return true;
                }
            }
            if (shape.Fill is not null)
            {
                return Discriminant(x, y, xr, yr) < 1;
            }
            return false;
        }
    }

}
