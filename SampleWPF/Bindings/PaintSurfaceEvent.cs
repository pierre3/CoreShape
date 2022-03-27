using CoreShape.Extensions.SkiaSharp;

namespace SampleWPF.ViewModels.Bindings
{
    internal record PaintSurfaceEvent
    {
        public SkiaGraphics Graphics { get; }
        public PaintSurfaceEvent(SkiaGraphics graphics)
        {
            Graphics = graphics;
        }
    }
}