using CoreShape.Extensions.SkiaSharp;
using Reactive.Bindings.Interactivity;
using SampleWPF.ViewModels.Bindings;
using SkiaSharp.Views.Desktop;
using System;
using System.Linq;
using System.Reactive.Linq;

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
