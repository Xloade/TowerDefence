using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    class SoldierList:IEnumerable<Soldier>
    {
        private List<Soldier> Soldiers;
        private DrawInfo DrawInfo;
        public SoldierList(List<Soldier> soldiers, DrawInfo drawInfo)
        {
            Soldiers = soldiers;
            DrawInfo = drawInfo;
        }

        public IEnumerator<Soldier> GetEnumerator()
        {
            return new SoldierEnumerator(Soldiers, DrawInfo);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
