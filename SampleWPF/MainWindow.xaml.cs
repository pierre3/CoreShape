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
using SampleWPF.ViewModels;

namespace SampleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? VM { get => DataContext as MainWindowViewModel; }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void SKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            VM?.ShapeManager.Draw(new SkiaGraphics(e.Surface.Canvas));
        }

        private void SKElement_MouseMove(object sender, MouseEventArgs e)
        {
            var currentPoint = GetMousePoint(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                VM?.ShapeManager.Drag(currentPoint);
                skElement.InvalidateVisual();
            }
            else
            {
                var hitResult = VM?.ShapeManager.HitTest(currentPoint);
                Cursor = SwitchCursor(hitResult);
            }
            VM?.ShapeManager.UpdateOldPoint(currentPoint);
        }

        private void SkElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            var currentPoint = GetMousePoint(e);
            VM?.ShapeManager.Locate(currentPoint);
            skElement.InvalidateVisual();
        }

        private void SkElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released)
            {
                return;
            }
            VM?.ShapeManager.Drop();
            skElement.InvalidateVisual();
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

        private static Cursor SwitchCursor(HitResult? hitResult)
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
    }
}
