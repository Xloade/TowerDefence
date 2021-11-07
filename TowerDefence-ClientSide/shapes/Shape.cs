using System;
using System.Drawing;

namespace TowerDefence_ClientSide
{
    class Shape : IDraw
    {
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Rotation { get; set; }

        public Image sprite;

        public Shape(Point center, float width, float height, float rotation, Image sprite) : this(center, width, height, sprite)
        {
            Rotation = rotation;
        }

        public Shape(Point center, float width, float height, Image sprite) : this(center)
        {
            Width = width;
            Height = height;
            this.sprite = sprite;
        }

        public Shape()
        {
        }
        public Shape(Point center)
        {
            CenterX = center.X;
            CenterY = center.Y;
        }
        public void Draw(Graphics gr)
        {
            int biggerSide = (int)(Math.Max(Width, Height) * 1.5);
            Bitmap bmp = new Bitmap(biggerSide, biggerSide);


            using (Graphics grImage = Graphics.FromImage(bmp))
            {
                grImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                grImage.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                //Rotate.        
                grImage.RotateTransform(Rotation);
                //Move image back.
                grImage.TranslateTransform(-(float)Width / 2, -(float)Height / 2);
                grImage.DrawImage(sprite, 0, 0, Width, Height);
            }

            gr.DrawImage(bmp, CenterX - (bmp.Width / 2), CenterY - (bmp.Height / 2), bmp.Width, bmp.Height);
        }
        // piešimas vykdomas išvestinėse klasėse


    }
}