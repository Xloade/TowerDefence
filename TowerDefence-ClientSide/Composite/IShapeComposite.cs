using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_ClientSide.Composite
{
    interface IShapeComposite
    {
        public bool isShape()
        {
            return true;
        }

        public void GroupDraw(Graphics gr);

        public Shape GetNextShape(long last);
    }
}
