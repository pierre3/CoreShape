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

        private IList<IShape> shapes = new[]{
            new OvalShape(new CoreShape.Rectangle(100, 100, 200, 150),new SKRegionOvalHitTestStrategy())
            {
                Stroke = new Stroke(CoreShape.Color.Red, 2),
                //Fill = new Fill(CoreShape.Color.LightSkyBlue)
            },
            new RectangleShape(new CoreShape.Rectangle(350, 100, 100, 150))
            {
                Stroke = new Stroke(CoreShape.Color.Black, 2),
                Fill = new Fill(CoreShape.Color.LightPink)
            },
        };
        private IShape? activeShape;
        private CoreShape.Point oldPoint;


        private void sKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var g = new SkiaGraphics(e.Surface.Canvas);
            g.ClearCanvas(CoreShape.Color.Ivory);
            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }
        }

        private void sKElement_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(skElement);
            var currentPoint = new CoreShape.Point((float)p.X, (float)p.Y);

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
                foreach (var shape in shapes)
                {
                    var resizeType = shape.HitTest(currentPoint);
                    switch (resizeType)
                    {
                        case ResizeType.ResizeAll:
                            Cursor = Cursors.SizeAll;
                            break;
                        case ResizeType.ResizeN:
                        case ResizeType.ResizeS:
                            Cursor = Cursors.SizeNS;
                            break;
                        case ResizeType.ResizeE:
                        case ResizeType.ResizeW:
                            Cursor = Cursors.SizeWE;
                            break;
                        case ResizeType.ResizeNW:
                        case ResizeType.ResizeSE:
                            Cursor = Cursors.SizeNWSE;
                            break;
                        case ResizeType.ResizeNE:
                        case ResizeType.ResizeSW:
                            Cursor = Cursors.SizeNESW;
                            break;
                    }
                    if (resizeType is not ResizeType.None)
                    {
                        activeShape = shape;
                        break;
                    }
                }
            }
            oldPoint = currentPoint;
        }
    }

}
