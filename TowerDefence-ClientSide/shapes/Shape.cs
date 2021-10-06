using System;
using System.Drawing;

class Shape
{
    public float CenterX { get; set; }
    public float CenterY { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public float Rotation { get; set; }

    public Image sprite;

    public Shape(Point center, float width, float height, float rotation, Image sprite) : this(center,  width,  height,  sprite)
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
    public virtual void Draw(Graphics gr)
    {
        Bitmap bmp = new Bitmap((int)(Width*1.5), (int)(Height * 1.5));


        using (Graphics grImage = Graphics.FromImage(bmp))
        {
            grImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            grImage.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            //Rotate.        
            grImage.RotateTransform(Rotation);
            //Move image back.
            grImage.TranslateTransform(-(float)Width / 2, -(float)Height / 2);
            grImage.DrawImage(sprite, 0,0,Width, Height);
        }

        gr.DrawImage(bmp, CenterX - (Width / 2), CenterY - (Height / 2), Width, Height);
    }
    // piešimas vykdomas išvestinėse klasėse


}