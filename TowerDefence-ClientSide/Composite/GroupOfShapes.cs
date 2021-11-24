using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_ClientSide.Composite
{
    class GroupOfShapes: IGroupedShape
    {
        public List<IGroupedShape> Shapes = new List<IGroupedShape>();
        public List<IGroupedShape> getShapes()
        {
            return Shapes;
        }

        public void GroupDraw(Graphics gr)
        {
            Shapes.ForEach((shape) => shape.GroupDraw(gr));
        }

        public bool isShape()
        {
            return false;
        }
    }
}
