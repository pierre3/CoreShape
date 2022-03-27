using CoreShape.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SampleWPF.Bindings.Converters
{
    internal class HitResultToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hitResult = (HitResult)(value ?? HitResult.None);
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
