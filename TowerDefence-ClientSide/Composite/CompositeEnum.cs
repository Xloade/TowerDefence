using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;
using TowerDefence_ClientSide;

namespace TowerDefence_ClientSide.Composite
{
    class CompositeEnum : IEnumerator<Shape>
    {
        private readonly IShapeComposite root;
        private Shape currShape;
        private long currId;

        public CompositeEnum(IShapeComposite platoon)
        {
            this.root = platoon;
            this.currId = -1;
        }

        public Shape Current
        {
            get
            {
                if (currShape == null)
                {
                    throw new InvalidOperationException("Enumerator doesn't have next");
                }
                return currShape;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            //nothing to dispose
        }

        public bool MoveNext()
        {
            currShape = root.GetNextShape(currId);
            if (currShape == null) return false;
            currId = currShape.Info.Id;
            return true;
        }

        public void Reset()
        {
            currId = -1;
        }
    }
}
