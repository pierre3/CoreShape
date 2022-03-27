using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreShape;

namespace SampleWPF.ViewModels.Bindings
{
    internal record MouseEvent
    {
        public Point MousePosition { get; init; }
        public bool IsLButtonPressed { get; init; }
        public bool IsLButtonReleased { get; init; }
        public Action? InvalidateVisual { get; init; }
    }
}
