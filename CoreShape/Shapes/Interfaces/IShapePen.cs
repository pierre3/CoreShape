using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public interface IShapePen : IDraggable, IDrawable, ILocatable
    {
        IShape CreateShape();
    }
}
