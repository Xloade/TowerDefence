using System;
using System.Drawing;

class Shape
{
    public float CenterX { get; set; }
    public float CenterY { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public Image sprite;

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
        gr.DrawImage(sprite, CenterX - (Width/2), CenterY - (Height / 2), Width, Height);
    }
    // piešimas vykdomas išvestinėse klasėse


}