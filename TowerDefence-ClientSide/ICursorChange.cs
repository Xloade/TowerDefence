using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TowerDefence_ClientSide
{
    interface ICursorChange
    {
        void OnCursorChanged(Cursor cursor, CursorState cursorState);
    }
}
