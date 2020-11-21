using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Graphics
{
    public class Fill
    {
        public Color Color { get; set; }

        public static readonly Fill NullObject = new NullFill();
        public bool IsNull => this is NullFill;
        protected sealed class NullFill : Fill
        { }
    }
}