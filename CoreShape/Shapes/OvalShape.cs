using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public partial class OvalShape : RectangleShape
    {
        public OvalShape() : this(new Rectangle())
        { }

        public OvalShape(Rectangle bounds) : base(bounds, new OvalHitTestStrategy())
        {
        }

        public OvalShape(Rectangle bounds, IHitTestStrategy hitTestStrategy)
            : base(bounds, hitTestStrategy)
        {
        }

        public OvalShape(Point location, Size size)
            : this(new Rectangle(location, size)) { }

        public OvalShape(float left, float top, float width, float height)
            : this(new Rectangle(left, top, width, height)) { }

        public override void Draw(IGraphics g)
        {
            if (Fill is not null)
            {
                g.FillOval(Bounds, Fill);
            }
            if (Stroke is not null)
            {
                g.DrawOval(Bounds, Stroke);
            }
            if (IsSelected)
            {
                g.DrawRectangle(Bounds, new Stroke(Color.Black, 1));
                ResizeHandles.Draw(g);
            }
        }

        public override IShape Copy(Size delta)
        {
            var bounds = new Rectangle
            {
                Location = new Point(
                    Bounds.Left + delta.Width,
                    Bounds.Top + delta.Height),
                Size = Bounds.Size
            };
            return new OvalShape(bounds, HitTestStrategy)
            {
                Stroke = Stroke,
                Fill = Fill
            };
        }

    }
}
