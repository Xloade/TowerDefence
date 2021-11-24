using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace TowerDefence_ClientSide
{
    public interface IDraw
    {
        public float CenterX { get;}
        public float CenterY { get;}
        public float Width { get; set; }
        public float Height { get; set; }

        public float Rotation { get;}
        public void Draw(Graphics gr);
    }
}
