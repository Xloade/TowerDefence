using System;
using System.Drawing;

abstract class Shape
{
    public string Code { get; }
    public float CenterX { get; set; }
    public float CenterY { get; set; }


    public Shape()
    {
    }
    public Shape(string code, float x, float y)
    {
        Code = code;
        CenterX = x;
        CenterY = y;
    }
    public Shape(float x, float y)
    {
        CenterX = x;
        CenterY = y;
    }
    public abstract void Draw(Graphics gr); 
    // piešimas vykdomas išvestinėse klasėse

    static private readonly char[] separators = { ' ', ',', ';', ':' };

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
}