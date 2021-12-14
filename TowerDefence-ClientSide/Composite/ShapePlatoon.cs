using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
        public readonly List<IShapeComposite> Shapes = new List<IShapeComposite>();

        public ShapePlatoon(PlatoonType platoonName)
        {
            PlatoonName = platoonName;
        }
        public List<IShapeComposite> GetShapes()
        {
            return Shapes;
        }

        public List<Shape> RemoveDeepestSelection()
        {
            List<Shape> rez = new List<Shape>();
            var selections =
                from shape in Shapes
                where shape is ShapePlatoon
                where ((ShapePlatoon) shape).PlatoonName == PlatoonType.Selected
                where ((ShapePlatoon) shape).Shapes.OfType<ShapePlatoon>()
                    .Where(x => x.PlatoonName == PlatoonType.Selected)
                    .ToList().Count == 0
                select shape;
            var selectionList = selections.ToList();

            foreach (var selection in selectionList.OfType<ShapePlatoon>())
            {
                rez.AddRange(selection.Shapes.OfType<Shape>());
                Shapes.Remove(selection);

            }
            Shapes.AddRange(rez);
            foreach (var platoon in Shapes.OfType<ShapePlatoon>())
            {
                rez.AddRange(platoon.RemoveDeepestSelection());
            }

            return rez;
        }

        public List<Shape> RemoveAllSelections()
        {
            List<Shape> rez = new List<Shape>();
            var selections =
                from shape in Shapes
                where shape is ShapePlatoon
                where ((ShapePlatoon)shape).PlatoonName == PlatoonType.Selected
                select shape;
            var selectionList = selections.ToList();
            foreach (var platoon in Shapes.OfType<ShapePlatoon>())
            {
                if (platoon.PlatoonName == PlatoonType.Selected)
                {
                    rez.AddRange(platoon.RemoveAllSelections());
                }
                else
                {
                    platoon.RemoveAllSelections();
                }
            }
            foreach (var selection in selectionList.OfType<ShapePlatoon>())
            {
                rez.AddRange(selection.Shapes.OfType<Shape>());
                Shapes.Remove(selection);
            }
            if(PlatoonName != PlatoonType.Selected) Shapes.AddRange(rez);
            return rez;
        }
        //return most root selected platoon
        public List<ShapePlatoon> RemoveAllRootSelections()
        {
            var selections =
                from shape in Shapes
                where shape is ShapePlatoon
                where ((ShapePlatoon)shape).PlatoonName == PlatoonType.Selected
                select shape;
            var selectionList = selections.OfType<ShapePlatoon>().ToList();
            List<ShapePlatoon> rez = new List<ShapePlatoon>();
            rez.AddRange(selectionList);
            foreach (var selection in selectionList)
            {
                Shapes.Remove(selection);
            }
            foreach (var shape in Shapes.OfType<ShapePlatoon>())
            {
                rez.AddRange(shape.RemoveAllRootSelections());
            }

            return rez;
        }

        public void RemoveDeepestSelections()
        {

        }
        public void GroupDraw(Graphics gr)
        {
            Shapes.ForEach((shape) => shape.GroupDraw(gr));
        }

        public bool IsShape()
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
                var childAnswers = Shapes.Select(x => x.GetNextShape(last));
                var seed = new Shape
                {
                    Info = new DrawInfo
                    {
                        Id = long.MaxValue
                    }
                };
                var bestFromAll = childAnswers.Aggregate(seed ,(best, next) =>
                    next != null && next.Info.Id > last && next.Info.Id < best.Info.Id ? next : best);
                return seed == bestFromAll ? null : bestFromAll;
            }
            catch (InvalidOperationException)
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
