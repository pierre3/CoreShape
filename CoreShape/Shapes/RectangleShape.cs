using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public class RectangleShape : IShape
    {
        public Rectangle Bounds { get; protected set; }
        public Stroke Stroke { get; set; } = Stroke.NullObject;
        public Fill Fill { get; set; } = Fill.NullObject;

        public RectangleShape(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public RectangleShape(Point location, Size size)
            : this(new Rectangle(location, size)) { }

        public RectangleShape(float left, float top, float width, float height)
            : this(new Rectangle(left, top, width, height)) { }

        public virtual void Draw(IGraphics g)
        {
            if (!Fill.IsNull)
            {
                g.FillRectangle(Bounds, Fill);
            }
            if (!Stroke.IsNull)
            {
                g.DrawRectangle(Bounds, Stroke);
            }

        }

        public virtual bool HitTest(Point p)
        {
            if( Bounds.Left <= p.X && p.X <= Bounds.Right 
                && Bounds.Top <= p.Y && p.Y <= Bounds.Bottom)
            {
                return true;
            }
            return false;
        }

        public void Drag(Point oldPointer, Point currentPointer)
        {
            var (dx, dy) = (currentPointer.X - oldPointer.X, currentPointer.Y - oldPointer.Y);
            Bounds = new Rectangle(Bounds.Left + dx, Bounds.Top + dy, Bounds.Size.Width, Bounds.Size.Height);
        }
    }
}
