using CoreShape.Graphics;
using CoreShape.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWPF.Models
{
    internal class ShapeManager
    {
        private readonly IList<IShape> shapes = new List<IShape>();
        private IDraggable? activeShape;
        private IShapePen? shapePen;

        public void Draw(IGraphics g)
        {
            g.ClearCanvas(CoreShape.Color.Ivory);
            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }
            if (activeShape is IShapePen shapePen)
            {
                shapePen.Draw(g);
            }
        }

        public void Drag(CoreShape.Point oldPoint, CoreShape.Point currentPoint)
        {
            activeShape?.Drag(oldPoint, currentPoint);
        }

        public HitResult HitTest(CoreShape.Point currentPoint)
        {
            activeShape = shapePen;
            var hitResult = HitResult.None;
            foreach (var shape in shapes.Reverse())
            {
                hitResult = shape.HitTest(currentPoint);
                if (hitResult is not HitResult.None)
                {
                    activeShape = shape;
                    break;
                }
            }
            return hitResult;
        }

        public void Locate(CoreShape.Point location)
        {
            if (activeShape is IShapePen shapePen)
            {
                shapePen.Locate(location);
            }
            foreach (var shape in shapes)
            {
                shape.IsSelected = shape == activeShape;
            }
        }

        public void Drop()
        {
            if (activeShape is null)
            {
                return;
            }
            activeShape.Drop();
            if (activeShape is IShapePen shapePen)
            {
                var shape = shapePen.CreateShape();
                if (shape is null)
                {
                    return;
                }
                shapes.Add(shape);
                activeShape = shape;
            }
        }

        public void SetDefaultPen()
        {
            shapePen = null;
        }

        public void SetRectanglePen()
        {
            shapePen = new ShapePen<RectangleShape>(
                new Stroke(CoreShape.Color.Red, 2.0f),
                new Fill(CoreShape.Color.LightSeaGreen));
        }

        public void SetOvalPen()
        {
            shapePen = new ShapePen<OvalShape>(
                new Stroke(CoreShape.Color.Green, 1.0f),
                new Fill(CoreShape.Color.LightYellow));
        }
    }
}
