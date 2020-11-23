namespace CoreShape.Graphics
{
    public interface IGraphics
    {
        void ClearCanvas(Color color);
        void DrawRectangle(Rectangle rectangle, Stroke stroke);
        void FillRectangle(Rectangle rectangle, Fill fill);
    }
}
