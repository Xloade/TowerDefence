using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Prototype
{
    class BulletShape : Shape
    {
        public BulletShape()
        {
            Width = 50;
            Height = 50;
            LazyImageDictionary lazyImageDictionary = new LazyImageDictionary();
            this.SpriteImage = lazyImageDictionary.Get(SpritePaths.GetBullet());
        }
    }
}
