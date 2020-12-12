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

        private IShape shape = new RectangleShape(100, 100, 200, 150)
        {
            Stroke = new Stroke(CoreShape.Color.Red, 2),
            Fill = new Fill(CoreShape.Color.LightSkyBlue)
        };
        private IShape? activeShape;
        private CoreShape.Point oldPoint;


        private void sKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var g = new SkiaGraphics(e.Surface.Canvas);
            g.ClearCanvas(CoreShape.Color.Ivory);
            shape.Draw(g);
        }

        private void sKElement_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(skElement);
            var currentPoint = new CoreShape.Point((float)p.X, (float)p.Y);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                activeShape?.Drag(oldPoint, currentPoint);
                skElement.InvalidateVisual();
            }
            else
            {
                if (shape.HitTest(currentPoint))
                {
                    Cursor = Cursors.SizeAll;
                    activeShape = shape;
                }
                else
                {
                    Cursor = Cursors.Arrow;
                    activeShape = null;
                }
            }
            oldPoint = currentPoint;
            
        }
    }

}
