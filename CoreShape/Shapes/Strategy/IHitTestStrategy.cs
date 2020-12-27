using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public interface IHitTestStrategy<in TShape> where TShape : IShape
    {
        bool HitTest(Point p, TShape shape);
    }
}
