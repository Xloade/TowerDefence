using System;
using System.Drawing;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    public class Shape : IDraw, ICloneable
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
            this.sprite = (Image)sprite.Clone();
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
            MyConsole.WriteLineWithCount("Drawing shape");
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
                lock (this)
                {
                    //bullet prototipe doesnt do deep enough copy
                    lock (sprite)
                    {
                        grImage.DrawImage(sprite, 0, 0, Width, Height);
                    }
                }
            }
            lock (gr)
            {
                gr.DrawImage(bmp, CenterX - (bmp.Width / 2), CenterY - (bmp.Height / 2), bmp.Width, bmp.Height);
            }
        }
        // piešimas vykdomas išvestinėse klasėse

    public object Clone()
    {
        return (Shape)this.MemberwiseClone();
    }

    }
}