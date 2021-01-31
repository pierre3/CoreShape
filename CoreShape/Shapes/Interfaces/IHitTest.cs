using CoreShape.Graphics;

namespace CoreShape.Shapes
{
    public interface IHitTest
    {
        HitResult HitTest(Point p);
    }
}
