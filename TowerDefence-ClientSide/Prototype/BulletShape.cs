using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Prototype
{
    class BulletShape : AmmunitionShapes
    {
        public override Shape Shape { get; set; }
        public override string Sprite { get; set; }
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override float Rotation { get; set; }
        public override Point Coordinates { get; set; }
        public override LazyImageDictionary lazyImageDictionary { get; set; }

        public BulletShape(Point coordinates, float rotation) : base (coordinates, rotation)
        {
            Coordinates = coordinates;
            Sprite = SpritePaths.getBullet();
            Width = 50;
            Height = 50;
            Rotation = rotation;
            lazyImageDictionary = new LazyImageDictionary();
            Shape = new Shape(Coordinates, Width, Height, Rotation, lazyImageDictionary.get(Sprite));
        }
    }
}
