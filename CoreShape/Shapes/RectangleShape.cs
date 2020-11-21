using CoreShape.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public class RectangleShape : IShape
    {
        public Rectangle Bounds { get; init; }
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

        public void Draw(IGraphics g)
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
    }
}
