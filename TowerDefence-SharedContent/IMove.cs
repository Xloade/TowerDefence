using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public interface IMove
    {
        void MoveForward(PlayerType playerType);
        bool IsOutOfMap(PlayerType playerType);
    }
}
