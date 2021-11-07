using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TowerDefence_ClientSide
{
    class LvlDrawDecorator : DrawDecorator
    {
        private int _level;
        public LvlDrawDecorator(IDraw decoratedDraw, int level) : base(decoratedDraw)
        {
            _level = level;
        }
        public override void Draw(Graphics gr)
        {
            base.Draw(gr);

            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            gr.DrawString($"lvl:{_level}", drawFont, drawBrush, CenterX - (Width/2)+10, CenterY + (Height / 2), drawFormat);
        }
    }
}
