using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SampleWPF.Bindings.Converters
{
    internal static class MouseEventArgsExtensions
    {
        public static CoreShape.Point ConvertMousePoint(this MouseEventArgs e)
        {
            var p = e.GetPosition(e.Source as IInputElement);
            var dpi = VisualTreeHelper.GetDpi(e.Device.ActiveSource.RootVisual);
            return new CoreShape.Point((float)(p.X * dpi.DpiScaleX), (float)(p.Y * dpi.DpiScaleY));
        }
    }
}
