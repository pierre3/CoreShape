using CoreShape.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public interface IShape
    {
        void Draw(IGraphics g);
    }
}
