using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_ClientSide
{
    abstract class DrawDecorator : IDraw
    {
        protected IDraw Wrapee;

        public float CenterX => Wrapee.CenterX;
        public float CenterY => Wrapee.CenterY;
        public float Width { get => Wrapee.Width;
            set => Wrapee.Width = value;
        }
        public float Height { get => Wrapee.Height;
            set => Wrapee.Height = value;
        }
        public float Rotation => Wrapee.Rotation;

        public DrawDecorator(IDraw component)
        {
            Wrapee = component;
        }
        public virtual void Draw(Graphics gr)
        {
            Wrapee.Draw(gr);
        }
    }
}
