using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.Notifiers
{
    public interface OnUpdated
    {
        void OnMapUpdated(Map map);
    }
}
