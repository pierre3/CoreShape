using CoreShape.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public class ShapePen<TShape> : IShapePen where TShape : IShape, new()
    {
        private TShape Shape { get; set; }
        private bool IsDragging { get; set; }

        private Stroke? Stroke { get; set; }
        private Fill? Fill { get; set; }
        private IHitTestStrategy? HitTestStrategy { get; set; }

        public ShapePen(Stroke? stroke, Fill? fill, IHitTestStrategy? hitTestStragegy = null)
        {
            Shape = new TShape()
            {
                Stroke = new Stroke(Color.Black, 1f)
            };
            Stroke = stroke;
            Fill = fill;
            HitTestStrategy = hitTestStragegy;
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
            var shape = Shape.Copy(new Size());
            shape.Stroke = Stroke;
            shape.Fill = Fill;
            if (HitTestStrategy is not null)
            {
                shape.HitTestStrategy = HitTestStrategy;
            }
            return shape;
        }
    }
}
