using CoreShape.Graphics;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShape.Shapes
{
    public class ResizeHandleCollection
    {
        protected IReadOnlyCollection<ResizeHandleBase> Items { get; set; }

        public ResizeHandleBase? ActiveHandle { get; protected set; }

        public ResizeHandleCollection(float width, float height)
        {
            Items = new ReadOnlyCollection<ResizeHandleBase>(
                new ResizeHandleBase[]
                {
                    new ResizeHandleN(new Rectangle(0, 0, width, height)),
                    new ResizeHandleNE(new Rectangle(0, 0, width, height)),
                    new ResizeHandleE(new Rectangle(0, 0, width, height)),
                    new ResizeHandleSE(new Rectangle(0, 0, width, height)),
                    new ResizeHandleS(new Rectangle(0, 0, width, height)),
                    new ResizeHandleSW(new Rectangle(0, 0, width, height)),
                    new ResizeHandleW(new Rectangle(0, 0, width, height)),
                    new ResizeHandleNW(new Rectangle(0, 0, width, height))
                });
        }

        public void SetLocation(Rectangle parentBounds)
        {
            foreach (var handle in Items)
            {
                handle.SetLocation(parentBounds);
            }
        }

        public HitResult HitTest(Point p)
        {
            foreach (var handle in Items)
            {
                var hitResult = handle.HitTest(p);
                if (hitResult is not HitResult.None)
                {
                    ActiveHandle = handle;
                    return hitResult;
                }
            }
            ActiveHandle = null;
            return HitResult.None;
        }

        public Rectangle Resize(Point p, Rectangle parentBounds)
        {
            return ActiveHandle?.Resize(p, parentBounds) ?? parentBounds;
        }

        public void Draw(IGraphics g)
        {
            foreach (var handle in Items)
            {
                handle.Draw(g);
            }
        }

        public void SetInitialActiveHandle()
        {
            ActiveHandle = Items.First(x=> x is ResizeHandleSE);
        }
    }
}
