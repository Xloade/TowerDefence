using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerDefence_ServerSide.Mediator
{
    public interface IMediator
    {
        public bool Paused();
        public void Pause();
    }
}
