using CoreShape.Shapes;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using SampleWPF.Models;
using SampleWPF.ViewModels.Bindings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SampleWPF.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private ShapeManager ShapeManager { get; } = new();

        public ReactiveProperty<HitResult> HitResult { get; } = new();

        public ReactiveCommand<MouseEvent> MouseMoveCommand { get; }
        public ReactiveCommand<MouseEvent> MouseDownCommand { get; }
        public ReactiveCommand<MouseEvent> MouseUpCommand { get; }
        //public ReactiveCommand<PaintSurfaceEvent> PaintSurfaceCommand { get; }

        public ReactiveCommand DefaultPenCommand { get; }
        public ReactiveCommand RectanglePenCommand { get; }
        public ReactiveCommand OvalPenCommand { get; }

        private readonly CompositeDisposable disposable = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel()
        {
            DefaultPenCommand = new ReactiveCommand().WithSubscribe(() => ShapeManager.SetDefaultPen()).AddTo(disposable);
            RectanglePenCommand = new ReactiveCommand().WithSubscribe(() => ShapeManager.SetRectanglePen()).AddTo(disposable);
            OvalPenCommand = new ReactiveCommand().WithSubscribe(() => ShapeManager.SetOvalPen()).AddTo(disposable);

            //PaintSurfaceCommand = new ReactiveCommand<PaintSurfaceEvent>()
            //    .WithSubscribe(args => ShapeManager.Draw(args.Graphics))
            //    .AddTo(disposable);

            MouseMoveCommand = new ReactiveCommand<MouseEvent>();
            MouseDownCommand = new ReactiveCommand<MouseEvent>();
            MouseUpCommand = new ReactiveCommand<MouseEvent>();
            SubscribeMouseMoveCommand();
            SubscribeMouseDownCommand();
            SubscribeMouseDragCommand();
            SubscribeMouseUpCommand();
        }

        private void SubscribeMouseMoveCommand()
        {
            MouseMoveCommand
                .Where(args => !args.IsLButtonPressed)
                .Subscribe(args => HitResult.Value = ShapeManager.HitTest(args.MousePosition))
                .AddTo(disposable);
        }

        private void SubscribeMouseDownCommand()
        {
            MouseDownCommand
                .Where(args => args.IsLButtonPressed)
                .Subscribe(args =>
                {
                    ShapeManager.Locate(args.MousePosition);
                    args.InvalidateVisual?.Invoke();
                })
                .AddTo(disposable);
        }

        private void SubscribeMouseDragCommand()
        {
            MouseMoveCommand
                .Pairwise()
                .Where(args => args.NewItem.IsLButtonPressed)
                .Subscribe(args =>
                {
                    ShapeManager.Drag(args.OldItem.MousePosition, args.NewItem.MousePosition);
                    args.NewItem.InvalidateVisual?.Invoke();
                }).AddTo(disposable);
        }

        private void SubscribeMouseUpCommand()
        {
            MouseUpCommand
                .Where(args => args.IsLButtonReleased)
                .Subscribe(args =>
                {
                    ShapeManager.Drop();
                    args.InvalidateVisual?.Invoke();
                })
                .AddTo(disposable);
        }

        public void Draw(CoreShape.Graphics.IGraphics graphics)
        {
            ShapeManager.Draw(graphics);
        }

        public void Dispose() => disposable.Dispose();

    }
}
