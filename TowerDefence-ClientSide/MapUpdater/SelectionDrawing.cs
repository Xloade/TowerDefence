using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace TowerDefence_ClientSide
{
    class SelectionDrawing
    {
        public MouseSelection Selection { get; set; } = new MouseSelection();
        public void Draw(Graphics gr)
        {
            if (!Selection.Selected) return;
            lock (gr)
            {
                gr.FillRectangle(new SolidBrush(Color.FromArgb(128, 255, 255, 0)), Selection.Left, Selection.Top, Selection.Right - Selection.Left, Selection.Bot - Selection.Top);
                gr.DrawRectangle(new Pen(Brushes.Orange, 5), Selection.Left,Selection.Top , Selection.Right - Selection.Left, Selection.Bot - Selection.Top);
            }
        }
    }
}
