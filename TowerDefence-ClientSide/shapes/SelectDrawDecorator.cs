using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide.shapes
{
    class SelectDrawDecorator : DrawDecorator
    {
        private ISelected Shape;
        public SelectDrawDecorator(IDraw decoratedDraw, ISelected shape) : base(decoratedDraw)
        {
            Shape = shape;
        }
        public override void Draw(Graphics gr)
        {
            base.Draw(gr);
            if (Shape.Selected)
            {
                MyConsole.WriteLineWithCount("|   DrawDecorator");
                Bitmap bmp = new Bitmap(100, 100);

                using (Graphics grImage = Graphics.FromImage(bmp))
                {
                    grImage.FillRectangle(new SolidBrush(Color.FromArgb(128, 255, 255, 0)), 0, 0, 100, 100);
                    grImage.DrawRectangle(new Pen(Brushes.Orange, 5), 0, 0, 100, 100);
                }
                lock (gr)
                {
                    gr.DrawImage(bmp, CenterX - (Width / 2), CenterY - (Height / 2), bmp.Width, bmp.Height);
                }
            }
        }
    }
}