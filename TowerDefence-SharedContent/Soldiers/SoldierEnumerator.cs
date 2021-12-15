using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    class SoldierEnumerator : IEnumerator<Soldier>
    {
        public List<Soldier> Soldiers;
        public Soldier Current => Soldiers[CurrentIndex];

        private int CurrentIndex;

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public SoldierEnumerator(List<Soldier> soldiers, DrawInfo info)
        {
            Soldiers = soldiers.OrderBy(x => Distance(x, info)).ToList();
            CurrentIndex = -1;
            MyConsole.WriteLineWithCount("Soldier Iterator created");
        }

        private double Distance(DrawInfo info1, DrawInfo info2)
        {
            var biggerX = Math.Max(info1.Coordinates.X, info2.Coordinates.X);
            var smallerX = Math.Min(info1.Coordinates.X, info2.Coordinates.X);
            var biggerY = Math.Max(info1.Coordinates.Y, info2.Coordinates.Y);
            var smallerY = Math.Min(info1.Coordinates.Y, info2.Coordinates.Y);
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
