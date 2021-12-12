using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TowerDefence_ClientSide.shapes;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    class PlatoonDecorator : DrawDecorator
    {
        private IShape Shape;
        public PlatoonDecorator(IDraw decoratedDraw, IShape shape) : base(decoratedDraw)
        {
            Shape = shape;
        }
        public override void Draw(Graphics gr)
        {
            string platoon = Shape.PlatoonType.ToString();
            base.Draw(gr);
            MyConsole.WriteLineWithCount("|   platoon wrapper");
            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            lock (gr)
            {
                gr.DrawString($"{platoon}", drawFont, drawBrush, CenterX + (Width / 2) + 50, CenterY + (Height / 2), drawFormat);
            }
        }
    }
}
