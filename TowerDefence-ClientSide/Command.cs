using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    public abstract class Command
    {
        public abstract void Do(TowerType towerType);
        public abstract void Undo();
    }
}
