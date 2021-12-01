using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Composite
{
    public class ShapePlatoon: IShapeComposite, IEnumerable<Shape>
    {
        public PlatoonType PlatoonName { get; set; }
        public List<IShapeComposite> Shapes = new List<IShapeComposite>();

        public ShapePlatoon(PlatoonType platoonName)
        {
            PlatoonName = platoonName;
        }
        public List<IShapeComposite> getShapes()
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

        IEnumerator<Shape> IEnumerable<Shape>.GetEnumerator()
        {
            return new CompositeEnum(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CompositeEnum(this);
        }

        public Shape GetNextShape(long last)
        {
            try
            {
                //select next shapes from children > find higher then last > find min
                //return Shapes.Select(x => x.GetNextShape(last))
                    //   .Aggregate((best, next) => next != null && best.Info.Id > last && next.Info.Id > best.Info.Id ? next : best);
                var childAnswers = Shapes.Select(x => x.GetNextShape(last));
                Shape seed = new Shape();
                seed.Info = new DrawInfo();
                seed.Info.Id = long.MaxValue;
                var BestFromAll = childAnswers.Aggregate(seed ,(best, next) =>
                    next != null && next.Info.Id > last && next.Info.Id < best.Info.Id ? next : best);
                return seed == BestFromAll ? null : BestFromAll;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }

        public void DeleteShape(Shape shape)
        {
            Shapes.RemoveAll(x => x == shape);
            Shapes.FindAll(x => x is ShapePlatoon).ForEach(x => x.DeleteShape(shape));
        }

        public void UpdatePlatoon(PlatoonType platoonType)
        {
            PlatoonType passedPlatoonType = PlatoonName == PlatoonType.Selected ? platoonType : PlatoonName;
            Shapes.ForEach(x => x.UpdatePlatoon(passedPlatoonType));
        }

        public void UpdateSelection(PlatoonType platoonType)
        {
            Shapes.ForEach(x => x.UpdateSelection(PlatoonName));
        }

        public void SaveSelection(MouseSelection mouseSelection)
        {
            Shapes.ForEach(shape => shape.SaveSelection(mouseSelection));
            var filteredShapes = Shapes.FindAll(shape => shape is Shape && 
                                                         ((Shape)shape).CenterX > mouseSelection.Left && ((Shape)shape).CenterX < mouseSelection.Right &&
                                                         ((Shape)shape).CenterY > mouseSelection.Top && ((Shape)shape).CenterY < mouseSelection.Bot);
            if (filteredShapes.Count > 0)
            {
                var selectedPlatoon = new ShapePlatoon(PlatoonType.Selected);
                Shapes.Add(selectedPlatoon);
                selectedPlatoon.Shapes.AddRange(filteredShapes);
                Shapes.RemoveAll(shape => shape is Shape &&
                                          ((Shape)shape).CenterX > mouseSelection.Left && ((Shape)shape).CenterX < mouseSelection.Right &&
                                          ((Shape)shape).CenterY > mouseSelection.Top && ((Shape)shape).CenterY < mouseSelection.Bot);
            }
        }
    }
}
