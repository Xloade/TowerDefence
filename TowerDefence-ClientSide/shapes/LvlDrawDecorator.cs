using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_ClientSide.shapes;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    class LvlDrawDecorator : DrawDecorator
    {
        private IShape Shape;
        public LvlDrawDecorator(IDraw decoratedDraw, IShape shape) : base(decoratedDraw)
        {
            Shape = shape;
        }
        public override void Draw(Graphics gr)
        {
            ILevel level = (ILevel)Shape.Info;
            base.Draw(gr);
            MyConsole.WriteLineWithCount("|   LVL wrapper");
            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            lock (gr)
            {
                gr.DrawString($"lvl:{level.Level}", drawFont, drawBrush, CenterX - (Width/2)+10, CenterY + (Height / 2), drawFormat);
            }
        }
    }
}
