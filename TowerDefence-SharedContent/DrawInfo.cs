using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent
{
    public class DrawInfo: IdableObject
    {
        public Point Coordinates { get; set; }
        public string Sprite { get; set; }
        public float Rotation { get; set; }
    }
}
