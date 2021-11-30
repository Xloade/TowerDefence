using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Prototype
{    class RocketShape : Shape
    {
        public RocketShape()
        {
            Width = 300;
            Height = 300;
            LazyImageDictionary lazyImageDictionary = new LazyImageDictionary();
            this.spriteImage = lazyImageDictionary.get(SpritePaths.getLaser());
        }
    }
}
