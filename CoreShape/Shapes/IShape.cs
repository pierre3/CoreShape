using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public interface IShape
    {
        void Draw(IGraphics g);
        bool HitTest(Point p);
        void Drag(Point oldPointer, Point currentPointer);
    }
}
