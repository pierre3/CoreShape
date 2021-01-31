using CoreShape.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public abstract class ResizeHandleBase : IDrawable, IHitTest
    {
        public HitResult HitResult { get; protected set; }
        public Rectangle Bounds { get; protected set; }
        public Stroke? Stroke { get; set; } = new Stroke(Color.Black, 1f);
        public Fill? Fill { get; set; } = new Fill(Color.White);

        protected ResizeHandleBase(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public abstract Rectangle Resize(Point p, Rectangle parentBounds);

        public abstract void SetLocation(Rectangle parentBounds);

        public void Draw(IGraphics g)
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

        public HitResult HitTest(Point p)
        {
            return (Bounds.Left <= p.X && p.X <= Bounds.Right
                    && Bounds.Top <= p.Y && p.Y <= Bounds.Bottom)
                    ? HitResult
                    : HitResult.None;
        }

        protected void MoveTo(Point center)
        {
            Bounds = new Rectangle(
                center.X - Bounds.Size.Width / 2,
                center.Y - Bounds.Size.Height / 2,
                Bounds.Size.Width,
                Bounds.Size.Height);
        }
    }
}
