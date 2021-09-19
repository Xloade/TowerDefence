using System;
using System.Drawing;

abstract class Shape
{
    public string Code { get; }
    public float CenterX { get; set; }
    public float CenterY { get; set; }
    public int HitBox1X { get; set; }//virsus (atstumas nuo figuros centro)
    public int HitBox2X { get; set; }//apacia
    public int HitBox1Y { get; set; }//kaire
    public int HitBox2Y { get; set; }//desne
    public Color Coloring { get; set; } = Color.Gray;

    public float LineWidth;

    int _CollitionX;
    public int CollitionX
    { get { return _CollitionX; }
        set {
            if((value == 1 && _CollitionX == -1) || (value == -1 && _CollitionX == 1))//jei figura atsairenkia i du dalykus priesingose pusese ji sustingsta
            {
                _CollitionX = 0;
                StepXFreeze = true;
            }
            else
            {
                _CollitionX = value;
            }
        }
    } //1= is virsaus -1= is apacios

    int _CollitionY;
    public int CollitionY
    {
        get { return _CollitionY; }
        set
        {
            if((value == 1 && _CollitionY == -1) || (value == -1 && _CollitionY == 1))
            {
                _CollitionY = 0;
                StepYFreeze = true;
            }
            else
            {
                _CollitionY = value;
            }
        }
    } //1 = is kaires -1= is desines
    public Shape()
    {
    }
    public Shape(string code, float x, float y)
    {
        Code = code;
        CenterX = x;
        CenterY = y;
        CollitionX = 0;
        CollitionY = 0;
    }
    public Shape(float x, float y)
    {
        CenterX = x;
        CenterY = y;
        CollitionX = 0;
        CollitionY = 0;
    }
    public float StepX { get; set; }
    public float StepY { get; set; }
    public bool StepXFreeze = false;
    public bool StepYFreeze = false;
    public void Move()
    {
        if (!StepXFreeze)
            CenterX += StepX;
        else
            StepXFreeze = false;

        if (!StepYFreeze)
            CenterY += StepY;
        else
            StepYFreeze = false;
    }
    public override string ToString()
    {
        string tn = this.GetType().Name;  // sužinome išvestinio tipo vardą
        return $"{tn,-10} {Code,-4} x={CenterX,3} y={CenterY,3} area={Area(),6:f1}";
    }
    public abstract double Area(); // skaičiavimas vykdomas išvestinėse klasėse
    public abstract void Draw(Graphics gr); 
    // piešimas vykdomas išvestinėse klasėse

    static private readonly char[] separators = { ' ', ',', ';', ':' };

    static public Shape Parse(string input)
    {
        string code = input.Substring(0, 4);  // pirmi 4 simboliai yra figūros kodas
        int[] a = Array.ConvertAll(input.Substring(5)  // skaičiai nuo 5 simbolio
                .Split(separators, StringSplitOptions.RemoveEmptyEntries),
                int.Parse);
        // jau turime figūros kodą parametrų skaičių masyvą
        // parašykite kodą, kuris tikrintų ar figūrai yra tinkamas elemenyų skaičius
        switch (code.Substring(0, 2))
        {
            case "KV":
                if (a.Length == 3)
                    return new Square(code, a[0], a[1], a[2]);
                return null;
            case "ST":
                if (a.Length == 4)
                    return new Rectangle(code, a[0], a[1], a[2], a[3]);
                return null;
            case "AP":
                if (a.Length == 3)
                    return new Circle(code, a[0], a[1], a[2]);
                return null;
            case "EL":
                if (a.Length == 4)
                    return new Ellipse(code, a[0], a[1], a[2], a[3]);
                return null;
            default:
                return null;
        }
    }
    // skaičiuoja atstumą nuo figūros centro iki taško, panaudojant Pitagoro teoremą
    public float Distance(float x, float y)
    {
        return (float)(Math.Sqrt(Math.Pow(x - CenterX, 2) + Math.Pow(y - CenterY, 2)));
    }
    // skaičiuoja atstumą iki kitos figūros centro, panaudojant esamą metodą
    public float Distance(Shape shape)
    {
        return Distance(shape.CenterX, shape.CenterY);
    }
    public void GetCollition(Shape obj, Bitmap drawArea)
    {
        if (!(this.Equals(obj)))
        {
            if (((CenterY - HitBox1Y + StepY >= obj.CenterY - obj.HitBox1Y + obj.StepY && CenterY - HitBox1Y + StepY <= obj.CenterY + obj.HitBox2Y + obj.StepY)
                    || (CenterY + HitBox2Y + StepY >= obj.CenterY - obj.HitBox1Y + obj.StepY && CenterY + HitBox2Y + StepY <= obj.CenterY + obj.HitBox2Y + obj.StepY))
                    ||
                    (CenterY - HitBox1Y + StepY <= obj.CenterY - obj.HitBox1Y + obj.StepY && CenterY - HitBox1Y + StepY <= obj.CenterY + obj.HitBox2Y + obj.StepY
                    && CenterY + HitBox2Y + StepY >= obj.CenterY - obj.HitBox1Y + obj.StepY && CenterY + HitBox2Y + StepY >= obj.CenterY + obj.HitBox2Y + obj.StepY))
            {
                if (CenterX - HitBox1X + StepX < obj.CenterX + obj.HitBox2X + obj.StepX && CenterX > obj.CenterX)
                {
                    CollitionX = 1;
                }
                else if (CenterX + HitBox2X + StepX > obj.CenterX - obj.HitBox1X + obj.StepX && CenterX < obj.CenterX)
                {
                    CollitionX = -1;
                }
            }
            if (((CenterX - HitBox1X + StepX >= obj.CenterX - obj.HitBox1X + obj.StepX && CenterX - HitBox1X + StepX <= obj.CenterX + obj.HitBox2X + obj.StepX)
                    || (CenterX + HitBox2X + StepX >= obj.CenterX - obj.HitBox1X + obj.StepX && CenterX + HitBox2X + StepX <= obj.CenterX + obj.HitBox2X + obj.StepX))
                    ||
                    (CenterX - HitBox1X + StepX <= obj.CenterX - obj.HitBox1X + obj.StepX && CenterX - HitBox1X + StepX <= obj.CenterX + obj.HitBox2X + obj.StepX
                    && CenterX + HitBox2X + StepX >= obj.CenterX - obj.HitBox1X + obj.StepX && CenterX + HitBox2X + StepX >= obj.CenterX + obj.HitBox2X + obj.StepX))
            {
                if (CenterY - HitBox1Y + StepY < obj.CenterY + obj.HitBox2Y + obj.StepY && CenterY > obj.CenterY)
                {
                    CollitionY = 1;
                }
                else if (CenterY + HitBox2Y + StepY > obj.CenterY - obj.HitBox1Y + obj.StepY && CenterY < obj.CenterY)
                {
                    CollitionY = -1;
                }
            }
        }
        if (CenterX - HitBox2X + StepX < 0)
        {
            CollitionX = 1;
        }   
        else if (CenterX + HitBox1X + StepX > drawArea.Width)
        {
            CollitionX = -1;
        }
        if (CenterY - HitBox2Y + StepY < 0)
        {
            CollitionY = 1;
        }
        else if (CenterY + HitBox1Y + StepY > drawArea.Height)
        {
            CollitionY = -1;
        }
    }
    public void SetCollition()
    {
        if (CollitionX == 1)
            StepX = Math.Abs(StepX);
        else if (CollitionX == -1)
            StepX = -Math.Abs(StepX);
        if (CollitionY == 1)
            StepY = Math.Abs(StepY);
        else if (CollitionY == -1)
            StepY = -Math.Abs(StepY);
        CollitionX = 0;
        CollitionY = 0;
    }
    public abstract double DistanceFromTop();

    public virtual void DrawLine(Graphics gr) { }
}