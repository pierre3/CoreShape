using CoreShape.Graphics;
using System;
using System.Collections.Generic;

namespace CoreShape.Shapes
{
    public class RectangleShape : IShape
    {
        public Rectangle Bounds { get; protected set; }
        public Stroke? Stroke { get; set; }
        public Fill? Fill { get; set; }

        public IHitTestStrategy HitTestStrategy { get; set; }
        
        protected ResizeHandleCollection ResizeHandles { get; set; }

        public bool IsSelected { get; set; }

        public RectangleShape()
            : this(new Rectangle())
        {
        }

        public RectangleShape(Rectangle bounds)
        {
            Bounds = bounds;
            HitTestStrategy = new RectangleHitTestStrategy();
            ResizeHandles = new ResizeHandleCollection(8, 8);
            ResizeHandles.SetLocation(Bounds);
        }

        public RectangleShape(Rectangle bounds, IHitTestStrategy hitTestStrategy)
        {
            Bounds = bounds;
            HitTestStrategy = hitTestStrategy;
            ResizeHandles = new ResizeHandleCollection(8, 8);
            ResizeHandles.SetLocation(Bounds);
        }

        public RectangleShape(Point location, Size size)
            : this(new Rectangle(location, size)) { }

        public RectangleShape(float left, float top, float width, float height)
            : this(new Rectangle(left, top, width, height)) { }

        public virtual void Draw(IGraphics g)
        {
            if (Fill is not null)
            {
                g.FillRectangle(Bounds, Fill);
            }
            if (Stroke is not null)
            {
                g.DrawRectangle(Bounds, Stroke);
            }
            if (IsSelected)
            {
                if (Stroke is null)
                {
                    g.DrawRectangle(Bounds, new Stroke(Color.Black, 1));
                }
                ResizeHandles.Draw(g);
            }
        }

        public virtual HitResult HitTest(Point p)
        {
            if (IsSelected)
            {
                var hitResult = ResizeHandles.HitTest(p);
                if (hitResult is not HitResult.None)
                {
                    return hitResult;
                }
            }
            return HitTestStrategy.HitTest(p, this) ? HitResult.Body : HitResult.None;
        }

        public virtual void Drag(Point oldPointer, Point currentPointer)
        {
            if (ResizeHandles.ActiveHandle is not null)
            {
                SetBounds(ResizeHandles.Resize(currentPointer, Bounds));
                return;
            }
            var (dx, dy) = (currentPointer.X - oldPointer.X, currentPointer.Y - oldPointer.Y);
            SetBounds(new Rectangle(Bounds.Left + dx, Bounds.Top + dy, Bounds.Size.Width, Bounds.Size.Height));
        }

        public void SetBounds(Rectangle bounds)
        {
            Bounds = bounds;
            ResizeHandles.SetLocation(bounds);
        }

        public void Drop()
        {
            var (left, top, width, height) = (Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height);
            if (Bounds.Width < 0)
            {
                left = Bounds.Right;
                width = Math.Abs(Bounds.Width);
            }
            if (Bounds.Height < 0)
            {
                top = Bounds.Bottom;
                height = Math.Abs(Bounds.Height);
            }
            SetBounds(new Rectangle(left, top, width, height));
        }

        public void Locate(Point location)
        {
            SetBounds(new Rectangle(location, new Size()));
            ResizeHandles.SetInitialActiveHandle();
        }

    }
}