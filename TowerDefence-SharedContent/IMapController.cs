using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public interface IMapController
    {
        void Attach(IMapObserver mapObserver);
        void Deattach(IMapObserver mapObserver);
        void Notify();
    }
}
