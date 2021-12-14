using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_ClientSide
{
    class LazyImageDictionary
    {
        private Dictionary<string, Image> images;

        public LazyImageDictionary()
        {
            this.images = new Dictionary<string, Image>();
        }

        public Image Get(string path)
        {
            if (!images.ContainsKey(path))
            {
                images.Add(path, Image.FromFile(path));
            }
            return images[path];
        }
    }
}
