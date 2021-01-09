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
        protected IReadOnlyCollection<ResizeHandle> Items { get; set; }

        public ResizeHandle? ActiveHandle { get; protected set; }

        public ResizeHandleCollection(float width, float height)
        {
            Items = new ReadOnlyCollection<ResizeHandle>(
                new ResizeHandle[]
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

        public ResizeType HitTest(Point p)
        {
            foreach (var handle in Items)
            {
                var resizeType = handle.HitTest(p);
                if (resizeType is not ResizeType.None)
                {
                    ActiveHandle = handle;
                    return resizeType;
                }
            }
            ActiveHandle = null;
            return ResizeType.None;
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
    }
}
