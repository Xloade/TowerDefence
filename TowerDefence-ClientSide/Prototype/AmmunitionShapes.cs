using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_ClientSide.Prototype;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    abstract class AmmunitionShapes : ICloneable
    {
        public abstract Shape Shape { get; set; }
        public abstract string Sprite { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public abstract float Rotation { get; set; }
        public abstract Point Coordinates { get; set; }
        public abstract LazyImageDictionary lazyImageDictionary { get; set; }

        public AmmunitionShapes (Point coordinates, float rotation)
        {
            lazyImageDictionary = new LazyImageDictionary();
            Coordinates = coordinates;
            Rotation = rotation;
        }       

        public object Clone()
        {
            return (AmmunitionShapes)this.MemberwiseClone();
        }

    }
}
