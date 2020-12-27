using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public class RectangleShape : IShape
    {
        public Rectangle Bounds { get; protected set; }
        public Stroke? Stroke { get; set; }
        public Fill? Fill { get; set; }
        protected IHitTestStrategy<RectangleShape> HitTestStrategy { get; set; }

        public RectangleShape(Rectangle bounds)
        {
            Bounds = bounds;
            HitTestStrategy = new RectangleHitTestStrategy();
        }

        public RectangleShape(Rectangle bounds, IHitTestStrategy<RectangleShape> hitTestStrategy)
        {
            Bounds = bounds;
            HitTestStrategy = hitTestStrategy;
        }

        public RectangleShape(Point location, Size size)
            : this(new Rectangle(location, size)) { }

        public RectangleShape(float left, float top, float width, float height)
            : this(new Rectangle(left, top, width, height)) { }

        public virtual void Draw(IGraphics g)
        {
            if (Fill is not null)
            {
                g.FillRectangle(Bounds, Fill);
            }
            if (Stroke is not null)
            {
                g.DrawRectangle(Bounds, Stroke);
            }
        }

        public virtual bool HitTest(Point p)
        {
            return HitTestStrategy.HitTest(p, this);
        }

        public virtual void Drag(Point oldPointer, Point currentPointer)
        {
            var (dx, dy) = (currentPointer.X - oldPointer.X, currentPointer.Y - oldPointer.Y);
            Bounds = new Rectangle(Bounds.Left + dx, Bounds.Top + dy, Bounds.Size.Width, Bounds.Size.Height);
        }
    }
}
