using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoreShape.Shapes;
using CoreShape.Graphics;
using SkiaSharp.Views.Desktop;
using CoreShape.Extensions.SkiaSharp;

namespace SampleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private IList<IShape> shapes = new List<IShape>();
        private IDraggable? activeShape;
        private CoreShape.Point oldPoint;
        private IShapePen? ShapePen;


        private void SKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var g = new SkiaGraphics(e.Surface.Canvas);
            g.ClearCanvas(CoreShape.Color.Ivory);
            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }
            if (activeShape is IShapePen shapePen)
            {
                shapePen.Draw(g);
            }
        }

private void SKElement_MouseMove(object sender, MouseEventArgs e)
{
    var currentPoint = GetMousePoint(e);
    if (e.LeftButton == MouseButtonState.Pressed)
    {
        if (activeShape is null)
        { return; }
        activeShape.Drag(oldPoint, currentPoint);
        skElement.InvalidateVisual();
    }
    else
    {
        activeShape = ShapePen;
        foreach (var shape in shapes.Reverse())
        {
            var hitResult = shape.HitTest(currentPoint);
            Cursor = SwitchCursor(hitResult);
            if (hitResult is not HitResult.None)
            {
                activeShape = shape;
                break;
            }
        }
    }
    oldPoint = currentPoint;
}

        private CoreShape.Point GetMousePoint(MouseEventArgs e)
        {
            var p = e.GetPosition(skElement);
            var dpi = VisualTreeHelper.GetDpi(this);
            var currentPoint = new CoreShape.Point(
                (float)(p.X * dpi.DpiScaleX),
                (float)(p.Y * dpi.DpiScaleY));
            return currentPoint;
        }

        private static Cursor SwitchCursor(HitResult hitResult)
        {
            return hitResult switch
            {
                HitResult.Body => Cursors.SizeAll,
                HitResult.ResizeN => Cursors.SizeNS,
                HitResult.ResizeS => Cursors.SizeNS,
                HitResult.ResizeE => Cursors.SizeWE,
                HitResult.ResizeW => Cursors.SizeWE,
                HitResult.ResizeNW => Cursors.SizeNWSE,
                HitResult.ResizeSE => Cursors.SizeNWSE,
                HitResult.ResizeNE => Cursors.SizeNESW,
                HitResult.ResizeSW => Cursors.SizeNESW,
                _ => Cursors.Arrow
            };
        }

        private void SkElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            if (activeShape is IShapePen shapePen)
            {
                var p = e.GetPosition(skElement);
                var dpi = VisualTreeHelper.GetDpi(this);
                var location = new CoreShape.Point((float)(p.X * dpi.DpiScaleX), (float)(p.Y * dpi.DpiScaleY));
                shapePen.Locate(location);
            }
            foreach (var shape in shapes)
            {
                shape.IsSelected = shape == activeShape;
            }
            skElement.InvalidateVisual();
        }

        private void SkElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released)
            {
                return;
            }
            if (activeShape is null)
            {
                return;
            }
            activeShape.Drop();
            if (activeShape is IShapePen shapePen)
            {
                var shape = shapePen.CreateShape();
                if (shape is null)
                {
                    return;
                }
                shapes.Add(shape);
                activeShape = shape;
            }
            skElement.InvalidateVisual();
        }

        private void DefaultButton_Checked(object sender, RoutedEventArgs e)
        {
            ShapePen = null;
        }

        private void RectButton_Checked(object sender, RoutedEventArgs e)
        {
            ShapePen = new ShapePen<RectangleShape>(
                new Stroke(CoreShape.Color.Red, 2.0f),
                new Fill(CoreShape.Color.LightSeaGreen));
        }

        private void OvalButton_Checked(object sender, RoutedEventArgs e)
        {
            ShapePen = new ShapePen<OvalShape>(
                new Stroke(CoreShape.Color.Green, 1.0f),
                new Fill(CoreShape.Color.LightYellow));
        }
    }
}
