using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings.Interactivity;
using System.Reactive.Linq;
using System.Windows.Input;
using SampleWPF.ViewModels.Bindings;
using System.Windows;

namespace SampleWPF.Bindings.Converters
{
    internal class MouseEventReactiveConverter : ReactiveConverter<MouseEventArgs, MouseEvent>
    {
        protected override IObservable<MouseEvent> OnConvert(IObservable<MouseEventArgs> source)
        {
            return source.Select(e => new MouseEvent
            {
                MousePosition = e.ConvertMousePoint(),
                IsLButtonPressed = e.LeftButton == MouseButtonState.Pressed,
                IsLButtonReleased = e.LeftButton == MouseButtonState.Released,
                InvalidateVisual = () => (AssociateObject as FrameworkElement)?.InvalidateVisual()
            });
        }
    }
}
