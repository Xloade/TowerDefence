using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    public class CursorCommand : Command
    {
        private GameCursor GameCursor;

        public CursorCommand(GameCursor gameCursor)
        {
            GameCursor = gameCursor;
        }
        public override void Do(TowerType towerType)
        {
            GameCursor.Change(towerType);
        }

        public override void Undo()
        {
            GameCursor.Reset();
        }
    }
}
