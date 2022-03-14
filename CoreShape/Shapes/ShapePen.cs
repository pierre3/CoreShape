using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public class ShapePen<TShape> : IShapePen where TShape : IShape, new()
    {
        private TShape Shape { get; set; }
        private bool IsDragging { get; set; }

        public IShape Template { get; set; }

        public ShapePen(TShape template)
        {
            Shape = new TShape()
            {
                Stroke = new Stroke(Color.Black, 1f)
            };
            Template = template;
        }

        public ShapePen(Stroke? stroke, Fill? fill, IHitTestStrategy? hitTestStragegy = null)
        {
            Shape = new TShape()
            {
                Stroke = new Stroke(Color.Black, 1f)
            };
            Template = new TShape()
            {
                Stroke = stroke,
                Fill = fill
            };
            if (hitTestStragegy is not null)
            {
                Template.HitTestStrategy = hitTestStragegy;
            }
        }

        public void Drag(Point oldPointer, Point currentPointer)
        {
            Shape.Drag(oldPointer, currentPointer);
        }

        public void Draw(IGraphics g)
        {
            if (IsDragging)
            {
                Shape.Draw(g);
            }
        }

        public void Drop()
        {
            Shape.Drop();
            IsDragging = false;
        }

        public void Locate(Point location)
        {
            Shape.Locate(location);
            IsDragging = true;
        }

        public IShape CreateShape()
        {
            var shape = new TShape()
            {
                Stroke = Template.Stroke,
                Fill = Template.Fill,
                HitTestStrategy = Template.HitTestStrategy,
                IsSelected = true
            };
            shape.SetBounds(Shape.Bounds);
            return shape;
        }
    }
}
