using System.Drawing;

class Square : Shape
{
    public int Length { get; set; }

    public Square(string code, int centerX, int centerY, int length) :
            base(code, centerX, centerY)
    {
        Length = length;
        HitBox1X = length / 2;
        HitBox2X = length / 2;
        HitBox1Y = length / 2;
        HitBox2Y = length / 2;
    }
    public override string ToString()
    {
        return $"{base.ToString()} L={Length,3} P={Perimeter(),3}";
    }
    public override double Area()
    {
        return Length * Length;
    }

    public virtual int Perimeter()
    {
        return Length * 4;
    }

    public override void Draw(Graphics gr)
    {
        Brush br = new SolidBrush(Coloring);
        gr.FillRectangle(br, CenterX - Length / 2, CenterY - Length / 2,
            Length, Length);
    }

    public override double DistanceFromTop()
    {
        return CenterY - Length / 2;
    }
}

