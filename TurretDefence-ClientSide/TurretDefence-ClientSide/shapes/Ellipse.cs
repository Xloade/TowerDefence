using System;
using System.Drawing;

class Ellipse : Circle
{
    public int DiameterY { get; set; }
    public Ellipse(string code, int centerX, int centerY, int dX, int dY) :
         base(code, centerX, centerY, dX)
    {
        DiameterY = dY;
        HitBox1X = dX / 2;
        HitBox2X = dX / 2;
        HitBox1Y = dY / 2;
        HitBox2Y = dY / 2;
    }
    public override string ToString()
    {
        return $"{base.ToString()} dY={DiameterY,3}";
    }
    public override double Area()
    {
        return Math.PI * DiameterX * DiameterY / 4;
    }
    public override void Draw(Graphics gr)
    {
        Brush br = new SolidBrush(Coloring);
        gr.FillEllipse(br, CenterX - DiameterX / 2, CenterY - DiameterY / 2,
                DiameterX, DiameterY);
    }
    public override double DistanceFromTop()
    {
        return CenterY - DiameterY / 2;
    }
    // https://www.mathsisfun.com/geometry/ellipse-perimeter.html
}