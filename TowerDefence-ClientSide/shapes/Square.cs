using System.Drawing;

class Square : Shape
{
    public int Length { get; set; }

    public Square(string code, int centerX, int centerY, int length) :
            base(code, centerX, centerY)
    {
        Length = length;
    }
    public override string ToString()
    {
        return $"{base.ToString()} L={Length,3} P={Perimeter(),3}";
    }

    public virtual int Perimeter()
    {
        return Length * 4;
    }

    public override void Draw(Graphics gr)
    {
        Brush br = new SolidBrush(Color.Red);
        gr.FillRectangle(br, CenterX - Length / 2, CenterY - Length / 2,
            Length, Length);
    }
}

