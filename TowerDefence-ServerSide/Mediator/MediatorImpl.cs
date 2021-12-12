using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerDefence_ServerSide.Mediator
{
    public class MediatorImpl : IMediator
    {
        public bool PauseState = false;

        public bool Paused()
        {
            return PauseState;
        }

        public void Pause()
        {
            if (PauseState) PauseState = false;
            else PauseState = true;
        }
    }
}
