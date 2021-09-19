using System.Drawing;

class Rectangle : Square
{
    public int Height { get; set; }
    public Rectangle(string code, int centerX, int centerY, int length, int height) :
            base(code, centerX, centerY, length)
    {
        Height = height;
        HitBox1X = length / 2;
        HitBox2X = length / 2;
        HitBox1Y = height / 2;
        HitBox2Y = height / 2;
    }

    public override string ToString()
    {
        return $"{base.ToString()} H={Height,3}";
    }
    public override double Area()
    {
        return Length * Height;
    }
    public override int Perimeter()
    {
        return (Length + Height)*2;
    }
    public override void Draw(Graphics gr)
    {
        Brush br = new SolidBrush(Coloring);
        gr.FillRectangle(br, CenterX - Length / 2, CenterY - Height / 2,
            Length, Height);
    }
    public override double DistanceFromTop()
    {
        return CenterY - Height / 2;
    }
}


