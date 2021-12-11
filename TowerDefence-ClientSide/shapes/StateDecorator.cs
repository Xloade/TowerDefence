using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.shapes
{
    class StateDecorator : DrawDecorator
    {
        private IShape Shape;
        public StateDecorator(IDraw decoratedDraw, IShape shape) : base(decoratedDraw)
        {
            Shape = shape;
        }
        public override void Draw(Graphics gr)
        {
            string text;
            if (Shape.Info.IsReloading)
            {
                text = "Reloading..";
            }
            else if (Shape.Info.IsOverheated)
            {
                text = "Cooling down..";
            }
            else text = "";

            base.Draw(gr);
            MyConsole.WriteLineWithCount("|   state wrapper");
            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Draw string to screen.
            lock (gr)
            {
                gr.DrawString($"{text}", drawFont, drawBrush, CenterX - (Width / 2) + 25, CenterY - (Height / 2) - 10, drawFormat);
            }
        }
    }
}
