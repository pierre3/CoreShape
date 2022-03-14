﻿using System;
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
        private IShapePen? Pen;


        private void sKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
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

        private void sKElement_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(skElement);
            var dpi = VisualTreeHelper.GetDpi(this);
            var currentPoint = new CoreShape.Point((float)(p.X*dpi.DpiScaleX), (float)(p.Y*dpi.DpiScaleY));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (activeShape is null)
                { return; }
                activeShape.Drag(oldPoint, currentPoint);
                skElement.InvalidateVisual();
            }
            else
            {
                Cursor = Cursors.Arrow;
                activeShape = null;
                foreach (var shape in shapes.Reverse())
                {
                    var hitResult = shape.HitTest(currentPoint);
                    switch (hitResult)
                    {
                        case HitResult.Body:
                            Cursor = Cursors.SizeAll;
                            break;
                        case HitResult.ResizeN:
                        case HitResult.ResizeS:
                            Cursor = Cursors.SizeNS;
                            break;
                        case HitResult.ResizeE:
                        case HitResult.ResizeW:
                            Cursor = Cursors.SizeWE;
                            break;
                        case HitResult.ResizeNW:
                        case HitResult.ResizeSE:
                            Cursor = Cursors.SizeNWSE;
                            break;
                        case HitResult.ResizeNE:
                        case HitResult.ResizeSW:
                            Cursor = Cursors.SizeNESW;
                            break;
                    }
                    if (hitResult is not HitResult.None)
                    {
                        activeShape = shape;
                        break;
                    }
                }
                if (activeShape is null)
                {
                    activeShape = Pen;
                }
            }
            oldPoint = currentPoint;
        }

        private void skElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            if (activeShape is IShapePen shapePen)
            {
                var p = e.GetPosition(skElement);
                var dpi = VisualTreeHelper.GetDpi(this);
                shapePen.Locate(new CoreShape.Point((float)(p.X * dpi.DpiScaleX), (float)(p.Y * dpi.DpiScaleY)));
            }
            foreach (var shape in shapes)
            {
                shape.IsSelected = shape == activeShape;
            }
            skElement.InvalidateVisual();
        }

        private void skElement_MouseUp(object sender, MouseButtonEventArgs e)
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
                if (shape.Bounds.Size != default)
                {
                    shape.IsSelected = true;
                    activeShape = shape;
                    shapes.Add(shape);
                }
                skElement.InvalidateVisual();
            }

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == DefaultButton)
            {
                Pen = null;
            }
            else if (sender == RectButton)
            {
                Pen = new ShapePen<RectangleShape>(
                    new Stroke(CoreShape.Color.Red, 2.0f),
                    new Fill(CoreShape.Color.LightSeaGreen));
            }
            else if (sender == OvalButton)
            {
                Pen = new ShapePen<OvalShape>(
                    new Stroke(CoreShape.Color.Green, 1.0f),
                    new Fill(CoreShape.Color.LightYellow));
            }
        }
    }
}
