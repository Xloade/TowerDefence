using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TowerDefence_ClientSide
{
    class HpDrawDecorator : DrawDecorator
    {
        private int _fullHp;
        private int _currentHp;
        public HpDrawDecorator(IDraw decoratedDraw, int fullHp,  int currentHp) : base(decoratedDraw)
        {
            _fullHp = fullHp;
            _currentHp = currentHp;
        }
        public override void Draw(Graphics gr)
        {
            base.Draw(gr);

            Bitmap bmp = new Bitmap(50, 10);

            using (Graphics grImage = Graphics.FromImage(bmp))
            {
                int green = (int)(bmp.Width / (_fullHp*1.0) * _currentHp);
                int red = (int)(bmp.Width / (_fullHp*1.0) * (_fullHp- _currentHp));

                grImage.FillRectangle(Brushes.Green, 0, 0, green, 10);
                grImage.FillRectangle(Brushes.Red, green, 0, red, 10);
            }

            gr.DrawImage(bmp, CenterX - (Width / 2)+25, CenterY - (Height / 2)-10, bmp.Width, bmp.Height);
        }
    }
}
