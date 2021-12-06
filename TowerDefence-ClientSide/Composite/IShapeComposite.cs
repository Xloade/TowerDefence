using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_ClientSide.Composite
{
    public interface IShapeComposite
    {
        public bool IsShape()
        {
            return true;
        }

        public void GroupDraw(Graphics gr);

        public Shape GetNextShape(long last);
        public void DeleteShape(Shape shape);
        public void UpdatePlatoon(PlatoonType platoonType);
    }
}
