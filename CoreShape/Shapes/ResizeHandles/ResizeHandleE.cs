﻿using System;

namespace CoreShape.Shapes
{
    public class ResizeHandleE : ResizeHandleBase
    {
        public ResizeHandleE(Rectangle bounds) : base(bounds)
        {
            HitResult = HitResult.ResizeE;
        }

        public override Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return new Rectangle(parentBounds.Left, parentBounds.Top, p.X - parentBounds.Left, parentBounds.Size.Height);
        }

        public override void SetLocation(Rectangle parentBounds)
        {
            var center = new Point(parentBounds.Right, parentBounds.Top + parentBounds.Size.Height/2);
            MoveTo(center);
        }
    }
}
