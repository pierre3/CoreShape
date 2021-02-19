using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public interface IHitTestStrategy
    {
        bool HitTest(Point p, IShape shape);
    }
}
