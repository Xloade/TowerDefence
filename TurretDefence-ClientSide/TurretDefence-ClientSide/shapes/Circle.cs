using System;
using System.Drawing;

class Circle : Shape
{
    public float DiameterX { get; set; }

    public Circle(string code, int centerX, int centerY, int dX) :
         base(code, centerX, centerY)
    {
        DiameterX = dX;
        HitBox1X = dX / 2;
        HitBox2X = dX / 2;
        HitBox1Y = dX / 2;
        HitBox2Y = dX / 2;
    }
    public Circle(float centerX, float centerY, Color color, float lineW) :
         base(centerX, centerY)
    {
        Coloring = color;
        LineWidth = lineW;
    }
    public override string ToString()
    {
        return $"{base.ToString()} dX={DiameterX,3}";
    }
    public override double Area()
    {
        return Math.PI * DiameterX * DiameterX / 4;
    }

    public override void Draw(Graphics gr)
    {
        Brush br = new SolidBrush(Coloring);
        gr.FillEllipse(br, CenterX - DiameterX / 2, CenterY - DiameterX / 2,
            DiameterX, DiameterX);
    }

    public override double DistanceFromTop()
    {
        return CenterY - DiameterX / 2;
    }

    public override void DrawLine(Graphics gr)
    {
        Pen pen = new Pen(Coloring, LineWidth);
        gr.DrawEllipse(pen, CenterX - DiameterX / 2, CenterY - DiameterX / 2,
            DiameterX, DiameterX);
    }
}
