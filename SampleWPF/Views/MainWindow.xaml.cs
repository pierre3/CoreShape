using CoreShape.Extensions.SkiaSharp;
using SampleWPF.ViewModels;
using SkiaSharp.Views.Desktop;
using System.Windows;

namespace SampleWPF.Views;

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
        => (DataContext as MainWindowViewModel)?.Draw(new SkiaGraphics(e.Surface.Canvas));
}
