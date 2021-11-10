using System;
using System.Windows.Forms;
using TowerDefence_SharedContent;
using TowerDefence_SharedContent.Towers;

namespace TowerDefence_ClientSide
{
    public class GameCursor
    {
        private PlayerType PlayerType;
        private ICursorChange CursorChange;

        public GameCursor(GameWindow gameWindow, PlayerType playerType)
        {
            CursorChange = gameWindow;
            PlayerType = playerType;
        }
        public void Change(TowerType towerType)
        {
            CursorChange.OnCursorChanged(new Cursor(SpritePaths.GetTowerCursor(PlayerType, towerType)), CursorState.Modified);
            MyConsole.WriteLineWithCount("Command: Do");
        }

        public void Reset()
        {
            CursorChange.OnCursorChanged(Cursors.Default, CursorState.Default);
            MyConsole.WriteLineWithCount("Command: Undo");
        }
    }
}
