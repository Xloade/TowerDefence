using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TowerDefence_ClientSide.shapes;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    class HpDrawDecorator : DrawDecorator
    {
        private IShape Shape;
        public HpDrawDecorator(IDraw decoratedDraw, IShape shape) : base(decoratedDraw)
        {
            Shape = shape;
        }
        public override void Draw(Graphics gr)
        {
            IHitpoints Hitpoints = (IHitpoints)Shape.Info;
            base.Draw(gr);
            MyConsole.WriteLineWithCount("|   Hp wrapper");
            Bitmap bmp = new Bitmap(50, 10);

            using (Graphics grImage = Graphics.FromImage(bmp))
            {
                int green = (int)(bmp.Width / (Hitpoints.CurrentLvlHitpoints* 1.0) * Hitpoints.CurrentHitpoints);
                int red = (int)(bmp.Width / (Hitpoints.CurrentLvlHitpoints * 1.0) * (Hitpoints.CurrentLvlHitpoints - Hitpoints.CurrentHitpoints));

                grImage.FillRectangle(Brushes.Green, 0, 0, green, 10);
                grImage.FillRectangle(Brushes.Red, green, 0, red, 10);
            }
            lock(gr)
            {
                gr.DrawImage(bmp, CenterX - (Width / 2)+25, CenterY - (Height / 2)-10, bmp.Width, bmp.Height);
            }
        }
    }
}
