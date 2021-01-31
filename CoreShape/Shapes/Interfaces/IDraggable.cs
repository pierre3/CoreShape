using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public interface IDraggable
    {
        void Drag(Point oldPointer, Point currentPointer);
        void Drop();
    }
}
