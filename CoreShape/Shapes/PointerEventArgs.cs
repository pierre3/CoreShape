using System;

namespace CoreShape.Shapes
{
    public class PointerEventArgs : EventArgs
    {
        public IShape Shape { get; }
        public Point Point { get; }

        public PointerEventArgs(Point point, IShape shape)
        {
            Point = point;
            Shape = shape;
        }
    }
}
