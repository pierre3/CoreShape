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
using SkiaSharp.Views.Desktop;
using CoreShape.Extensions.SkiaSharp;

namespace SampleWPF.Bindings.Converters
{
    internal class SKPaintSurfaceEventReactiveConverter : ReactiveConverter<SKPaintSurfaceEventArgs, PaintSurfaceEvent>
    {
        protected override IObservable<PaintSurfaceEvent> OnConvert(IObservable<SKPaintSurfaceEventArgs> source)
        {
            return source.Select(args => new PaintSurfaceEvent(new SkiaGraphics(args.Surface.Canvas)));
        }
    }
}
