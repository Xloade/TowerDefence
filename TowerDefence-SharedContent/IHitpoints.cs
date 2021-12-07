using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public interface IHitpoints
    {
        public int CurrentHitpoints { get; set; }
        public int CurrentLvlHitpoints { get;}
    }
}
