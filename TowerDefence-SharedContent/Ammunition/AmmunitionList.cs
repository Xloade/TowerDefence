using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Ammunition
{
    class AmmunitionList:IEnumerable<Ammunition>
    {
        private List<Ammunition> Ammunitions;
        private DrawInfo DrawInfo;
        public AmmunitionList(List<Ammunition> ammunitions)
        {
            Ammunitions = ammunitions;
        }

        public IEnumerator<Ammunition> GetEnumerator()
        {
            return new AmmunitionEnumerator(Ammunitions);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
