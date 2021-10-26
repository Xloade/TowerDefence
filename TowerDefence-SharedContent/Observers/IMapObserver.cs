using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Observers
{
    public interface IMapObserver
    {
        void UpdateClient(Map map);
        void NotifyServer(String message);
    }
}
