using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace TowerDefence_ClientSide
{
    interface IDraw
    {
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public float Rotation { get; set; }
        public void Draw(Graphics gr);
    }
}
