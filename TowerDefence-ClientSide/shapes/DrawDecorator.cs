using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_ClientSide
{
    abstract class DrawDecorator : IDraw
    {
        protected IDraw wrapee;

        public float CenterX { get { return wrapee.CenterX; } set { wrapee.CenterX = value; } }
        public float CenterY { get { return wrapee.CenterY; } set { wrapee.CenterY = value; } }
        public float Width { get { return wrapee.Width; } set { wrapee.Width = value; } }
        public float Height { get { return wrapee.Height; } set { wrapee.Height = value; } }
        public float Rotation { get { return wrapee.Rotation; } set { wrapee.Rotation = value; } }

        public DrawDecorator(IDraw component)
        {
            wrapee = component;
        }
        public virtual void Draw(Graphics gr)
        {
            wrapee.Draw(gr);
        }
    }
}
