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

        private void SKElement_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var g = new SkiaGraphics(e.Surface.Canvas);
            var shape = new RectangleShape(100,100,200,150)
            {
                Stroke = new Stroke() { Color = CoreShape.Color.Red, Width = 2 },
                Fill = new Fill() { Color = CoreShape.Color.LightSkyBlue }
            };
            shape.Draw(g);
        }
    }
}
