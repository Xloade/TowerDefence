using System;
using System.Drawing;
using TowerDefence_SharedContent;

namespace TowerDefence_ClientSide
{
    public class Shape : IDraw, ICloneable, Composite.IShapeComposite
    {
        public float Width { get; set; }
        public float Height { get; set; }



        public DrawInfo Info { get; set; }
        public float CenterX { get{ return Info.Coordinates.X; } }
        public float CenterY { get { return Info.Coordinates.Y; } }
        public float Rotation { get { return Info.Rotation; } }

        public Image spriteImage;

        public IDraw DecoratedDrawInterface { get; set; }

        public Shape(DrawInfo drawInfo, float width, float height, Image spriteImage)
        {
            Width = width;
            Height = height;
            this.spriteImage = (Image)spriteImage.Clone();
            Info = drawInfo;
            DecoratedDrawInterface = this;
        }
        public Shape()
        {

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
                    lock (spriteImage)
                    {
                        grImage.DrawImage(spriteImage, 0, 0, Width, Height);
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
        public void DecoratedDraw(Graphics gr)
        {
            DecoratedDrawInterface.Draw(gr);
        }
        public void GroupDraw(Graphics gr)
        {
            DecoratedDraw(gr);
        }

        public Shape GetNextShape(long last)
        {
            return this;
        }
    }
}