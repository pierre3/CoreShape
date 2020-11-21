using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Graphics
{
    public class Stroke
    {
        public Color Color { get; set; }
        public float Width { get; set; }

        public static readonly Stroke NullObject = new NullStroke();
        public bool IsNull => this is NullStroke;
        protected sealed class NullStroke : Stroke { }
    }
}
