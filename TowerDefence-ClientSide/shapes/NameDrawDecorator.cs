using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    class NameDrawDecorator : DrawDecorator
    {
        private string _name;
        public NameDrawDecorator(IDraw decoratedDraw, string name) : base(decoratedDraw)
        {
            _name = name;
        }
        public override void Draw(Graphics gr)
        {
            base.Draw(gr);
            MyConsole.WriteLineWithCount("|   Name wrapper");
            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            lock (gr)
            {
                gr.DrawString($"{_name}", drawFont, drawBrush, CenterX + (Width / 2)+50, CenterY + (Height / 2), drawFormat);
            }
        }
    }
}
