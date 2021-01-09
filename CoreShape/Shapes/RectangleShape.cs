using CoreShape.Graphics;
using System.Collections.Generic;

namespace CoreShape.Shapes
{
    public class RectangleShape : IShape
    {
        public Rectangle Bounds { get; protected set; }
        public Stroke? Stroke { get; set; }
        public Fill? Fill { get; set; }

        protected IHitTestStrategy<RectangleShape> HitTestStrategy { get; set; }
        protected ResizeHandleCollection ResizeHandles { get; set; }

        public RectangleShape(Rectangle bounds)
        {
            Bounds = bounds;
            HitTestStrategy = new RectangleHitTestStrategy();
            ResizeHandles = new ResizeHandleCollection(8, 8);
            ResizeHandles.SetLocation(Bounds);
        }

        public RectangleShape(Rectangle bounds, IHitTestStrategy<RectangleShape> hitTestStrategy)
        {
            Bounds = bounds;
            HitTestStrategy = hitTestStrategy;
            ResizeHandles = new ResizeHandleCollection(8, 8);
            ResizeHandles.SetLocation(Bounds);
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
            ResizeHandles.Draw(g);
        }

        public virtual ResizeType HitTest(Point p)
        {
            var resizeType = ResizeHandles.HitTest(p);
            if (resizeType is not ResizeType.None)
            {
                return resizeType;
            }
            return HitTestStrategy.HitTest(p, this) ? ResizeType.ResizeAll : ResizeType.None;
        }

        public virtual void Drag(Point oldPointer, Point currentPointer)
        {
            if (ResizeHandles.ActiveHandle is not null)
            {
                SetBounds(ResizeHandles.Resize(currentPointer, Bounds));
                return;
            }
            var (dx, dy) = (currentPointer.X - oldPointer.X, currentPointer.Y - oldPointer.Y);
            SetBounds(new Rectangle(Bounds.Left + dx, Bounds.Top + dy, Bounds.Size.Width, Bounds.Size.Height));
        }

        protected void SetBounds(Rectangle bounds)
        {
            Bounds = bounds;
            ResizeHandles.SetLocation(bounds);
        }
    }
}