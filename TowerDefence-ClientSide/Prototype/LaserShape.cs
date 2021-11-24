using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Prototype
{
    class LaserShape : Shape
    {
        public LaserShape()
        {
            Width = 50;
            Height = 500;
            LazyImageDictionary lazyImageDictionary = new LazyImageDictionary();
            this.spriteImage = lazyImageDictionary.get(SpritePaths.getLaser());
        }
    }
}
