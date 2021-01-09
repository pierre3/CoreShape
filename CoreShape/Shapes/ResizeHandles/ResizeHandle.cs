using CoreShape.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public abstract class ResizeHandle : IShape
    {
        public ResizeType Type { get; protected set; }
        public Rectangle Bounds { get; protected set; }
        public Stroke? Stroke { get; set; } = new Stroke(Color.Black, 1f);
        public Fill? Fill { get; set; } = new Fill(Color.White);

        protected ResizeHandle(Rectangle bounds)
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

        public ResizeType HitTest(Point p)
        {
            return (Bounds.Left <= p.X && p.X <= Bounds.Right
                    && Bounds.Top <= p.Y && p.Y <= Bounds.Bottom)
                    ? Type
                    : ResizeType.None;
        }

        public void Drag(Point oldPointer, Point currentPointer)
        {
            throw new NotImplementedException();
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
