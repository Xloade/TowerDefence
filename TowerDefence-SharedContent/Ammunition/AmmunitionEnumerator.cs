using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TowerDefence_SharedContent.Ammunition
{
    class AmmunitionEnumerator:IEnumerator<Ammunition>
    {
        public List<Ammunition> Soldiers;
        public Ammunition Current => Soldiers[CurrentIndex];

        private int CurrentIndex;

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public AmmunitionEnumerator(List<Ammunition> ammunition)
        {
            Soldiers = ammunition.OrderByDescending(x => DistanceFromCenter(x)).ToList();
            CurrentIndex = -1;
        }

        private double DistanceFromCenter(DrawInfo info1)
        {
            var middlePoint = new Point(500, 350);
            var biggerX = Math.Max(info1.Coordinates.X, middlePoint.X);
            var smallerX = Math.Min(info1.Coordinates.X, middlePoint.X);
            var biggerY = Math.Max(info1.Coordinates.Y, middlePoint.Y);
            var smallerY = Math.Min(info1.Coordinates.Y, middlePoint.Y);
            return Math.Sqrt(Math.Pow(biggerX - smallerX, 2) + Math.Pow(biggerY - smallerY, 2));
        }
        public void Dispose()
        {
            //no need
        }

        public bool MoveNext()
        {
            if (CurrentIndex < Soldiers.Count)
            {
                CurrentIndex++;

            }
            return CurrentIndex < Soldiers.Count;
        }
        public void Reset()
        {
            CurrentIndex = -1;
        }
    }
}
