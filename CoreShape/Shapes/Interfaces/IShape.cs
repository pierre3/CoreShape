using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public interface IShape : IDrawable, IDraggable, IHitTest, ILocatable
    {
        Rectangle Bounds { get; }
        Stroke? Stroke { get; set; }
        Fill? Fill { get; set; }
        bool IsSelected { get; set; }
        IHitTestStrategy HitTestStrategy { get; set; }
        IShape Copy(Size delta);
    }
}
