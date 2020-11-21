using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Graphics
{
    public interface IGraphics
    {
        void DrawRectangle(Rectangle rectangle, Stroke stroke);
        void FillRectangle(Rectangle rectangle, Fill fill);
    }
}
