using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public interface IShape : IDrawable, IDraggable, IHitTest
    {
        bool IsSelected { get; set; }
    }
}
