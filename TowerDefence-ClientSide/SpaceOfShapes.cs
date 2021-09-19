using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Console;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

using Microsoft.AspNetCore.SignalR.Client;

enum Player
{
    PLAYER1,
    PLAYER2
}

class SpaceOfShapes : StartDraw
{
    List<Shape> shapes = new List<Shape>();
    private int gridSize = 50;
    List<Circle> explotionCircles = new List<Circle>();
    float explotionSpeed = 30f;

    HubConnection connection;
    Player playerType;
    public SpaceOfShapes(Player playerType) : base(Color.Cyan, "Demo C# 1 kursas",
        1000, 700, "Clear", "Grid", "Draw", "MoveAll", "Stop", "Print", "ColorUp", "ColorNear")
    {
        this.playerType = playerType;
        startSignalR();
    }
    private void startSignalR()
    {
        connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:53353/GameServer")
                .Build();
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Graphics gr = Graphics.FromImage(DrawArea);
            switch (message.ToLower())
            {
                case "clear":
                    ClearArea();
                    break;
                case "grid":
                    DrawGrid(gr);
                    break;
                case "draw":
                    DrawAll(gr);
                    break;
                case "moveAll":
                    if (shapes.Count == 0)
                    {
                        DrawAll(gr);
                        ClearArea();
                    }
                    MoveStartMulti();
                    break;
                case "stop":
                    physicsTimer.Enabled = false;
                    graphicalTimer.Enabled = false;
                    break;
                case "print":
                    Trial2();
                    break;
                case "colorUp":
                    ColorHigher(gr);
                    break;
                case "colorNear":
                    DrawLineBetweenNearest(gr, new Pen(Color.Blue, 2));
                    break;
                default:
                    break;
            }
            Refresh();
        });
    }
    protected override void physicsTimer_Tick(object sender, EventArgs e)
    {
        Console.WriteLine($"{shapes[4].StepY}");

        foreach (Shape shape in shapes)
        {
            foreach (Shape shape1 in shapes)
            {
                shape.GetCollition(shape1, DrawArea);
            }
        }
        foreach (Shape shape in shapes)
        {
            shape.SetCollition();
            shape.Move();
        }
        SpeedDecay(0.005F);
        float areaCrossDistance = (float)Math.Sqrt(Math.Pow(DrawArea.Height, 2) + Math.Pow(DrawArea.Width, 2));
        for (int i = 0; i < explotionCircles.Count; i++)
        {
            if (explotionCircles[i].DiameterX < areaCrossDistance*2)
            {
                float tranparancy = 1 - explotionCircles[i].DiameterX / (areaCrossDistance * 2);
                explotionCircles[i].Coloring = Color.FromArgb((int)(255 * tranparancy), 255, 0, 0);
                explotionCircles[i].DiameterX += explotionSpeed;
            }
                
            else
                explotionCircles.RemoveAt(i);
        }
    }
    protected override void graphicalTimer_Tick(object sender, EventArgs e)
    {
        Refresh();
    }
    protected override void Form1_Paint(object sender, PaintEventArgs e)
    {
        Graphics gr = e.Graphics;
        gr.SmoothingMode = SmoothingMode.AntiAlias;
        gr.DrawImage(DrawArea, 0, 0, DrawArea.Width, DrawArea.Height);
        foreach (Shape shape in shapes)
        {
            shape.Draw(gr);
        }
        foreach (Shape shape in explotionCircles)
        {
            shape.DrawLine(gr);
        }
        //gr.Dispose();
    }
    void DrawGrid(Graphics gr)
    {
        for (int x = 0; x < DrawArea.Width; x += gridSize)
            gr.DrawLine(Pens.Gray, x, 0, x, DrawArea.Height - 1);
        for (int y = 0; y < DrawArea.Height; y += gridSize)
            gr.DrawLine(Pens.Gray, 0, y, DrawArea.Width - 1, y);
    }
    protected override void btn_Click(object sender, EventArgs e)
    {
        Graphics gr = Graphics.FromImage(DrawArea);
        switch ((sender as Button).Name)
        {
            case "Clear":
                ClearArea();
                break;
            case "Grid":
                DrawGrid(gr);
                break;
            case "Draw":
                DrawAll(gr);
                break;
            case "MoveAll":
                if(shapes.Count ==0)
                {
                    DrawAll(gr);
                    ClearArea();
                }
                MoveStartMulti();
                break;
            case "Stop":
                physicsTimer.Enabled = false;
                graphicalTimer.Enabled = false;
                break;
            case "Print":
                Trial2();
                break;
            case "ColorUp":
                ColorHigher(gr);
                break;
            case "ColorNear":
                DrawLineBetweenNearest(gr, new Pen(Color.Blue, 2));
                break;
            default:
                break;
        }
        Refresh();
    }

    string[] dataOfShapes = // demonstraciniai pavyzdžiai
    {
        "KV01  550 300  40",
        "EL11  100 250; 50 80",
        "XX66   700   500   44",
        "KV45   700  50  50",
        "AP12  800 350 90",
        "ST11  270 600; 50 100"
    };

    void AddShapes(string[] input)
    {
        foreach (var data in input)
        {
            var shape = Shape.Parse(data);
            if (shape != null)
            {
                shapes.Add(shape);
            }
            else
                WriteLine("!!! Klaida eilutėje -->" + data);
        }
    }
    void Trial1()
    {
        var c0 = new Circle("AP01", 8, 5, 10);
        var e0 = new Ellipse("EL01", 8, 5, 10, 20);
        WriteLine(String.Join("\n", c0, e0));
    }
    void Trial2()
    {
        AddShapes(dataOfShapes);
        WriteLine("*** Pradinis sąrašas");
        shapes.ForEach(WriteLine);
        shapes.Sort((s1, s2) => s1.Code.CompareTo(s2.Code));
        WriteLine("*** Surikiuota pagal kodus");
        shapes.ForEach(WriteLine);
        shapes.Sort((s1, s2) => s1.Area().CompareTo(s2.Area()));
        WriteLine("*** Surikiuota pagal plotus");
        shapes.ForEach(WriteLine);
    }
    void DrawAll(Graphics gr)
    {
        AddShapes(dataOfShapes);
        foreach (Shape sh in shapes)
            sh.Draw(gr);
    }
    public static float Distance(float x1, float y1, float x2, float y2)
    {
        return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }

    public Shape ClosestToOrigin()//Atdranda arciause nuo Space kordinaciu
    {
        Shape closesObj = null;
        double closestDist = double.MaxValue;
        foreach (Shape shape in shapes)
        {
            double dist = Distance(Width, Height, shape.CenterX, shape.CenterY);
            if (dist < closestDist)
            {
                closestDist = dist;
                closesObj = shape;
            }
        }
        return closesObj;
    }

    public Shape ClosestToCenter()//Atdranda arciause nuo 0;0 kordinaciu
    {
        Shape closesObj = null;
        double closestDist = double.MaxValue;
        foreach (Shape shape in shapes)
        {
            double dist = Distance(0, 0, shape.CenterX + Width, shape.CenterY + Height);
            if (dist < closestDist)
            {
                closestDist = dist;
                closesObj = shape;
            }
        }
        return closesObj;
    }

    public void FindTwoClosest(ref Shape obj1, ref Shape obj2)//atranda dvi arciauses viena nuo kitos figuras
    {
        double closestDist = double.MaxValue;
        foreach (Shape shape1 in shapes)
        {
            foreach (Shape shape2 in shapes)
            {
                double dist = Distance(shape1.CenterX, shape1.CenterY, shape2.CenterX, shape2.CenterY);
                if (dist < closestDist && shape1 != shape2)
                {
                    closestDist = dist;
                    obj1 = shape1;
                    obj2 = shape2;
                }
            }
        }
    }
    public Shape FindHigest()
    {
        Shape Highest = null;
        double highestPos = double.MaxValue;
        foreach (Shape shape in shapes)
        {
            if (shape.DistanceFromTop() < highestPos)
            {
                Highest = shape;
                highestPos = shape.DistanceFromTop();
            }
        }
        return Highest;
    }
    public void ColorHigher(Graphics graph)
    {
        Shape Highest = FindHigest();
        Highest.Coloring = Color.Red;
        Highest.Draw(graph);
    }
    public void DrawLineBetweenNearest(Graphics graph, Pen pen)
    {
        Shape shape1 = null, shape2 = null;
        FindTwoClosest(ref shape1, ref shape2);
        graph.DrawLine(pen, shape1.CenterX, shape1.CenterY, shape2.CenterX, shape2.CenterY);
    }
    public void MoveStartMulti()
    {
        Random rand = new Random(DateTime.Now.Millisecond);
        List<Color> colors = new List<Color>();
        colors.Add(Color.Green);
        colors.Add(Color.Orange);
        colors.Add(Color.White);
        colors.Add(Color.Bisque);

        for (int i = 0; i < shapes.Count; i++)
        {
            shapes[i].Coloring = colors[i % colors.Count];
            shapes[i].StepX = rand.Next(-10000, 10000) / 10000F;
            shapes[i].StepY = rand.Next(-10000, 10000) / 10000F;
        }
        physicsTimer.Enabled = true;
        graphicalTimer.Enabled = true;
    }
    public void Explotion(int x, int y, float strenth, float speed , Color color, float lineW)
    {
        explotionSpeed = speed;
        Circle explotionCircle = new Circle(x, y, color, lineW);
        explotionCircle.Coloring = color;
        explotionCircle.DiameterX = 1;
        explotionCircles.Add(explotionCircle);


        foreach (Shape shape in shapes)
        {
            float radAngle = (float)Math.Atan2((shape.CenterY - y), (shape.CenterX - x));
            float angle = -radAngle * 180 / (float)Math.PI;
            float distance = Distance(x, y, shape.CenterX, shape.CenterY);
            float areaCrossDistance = (float)Math.Sqrt(Math.Pow(DrawArea.Height, 2) + Math.Pow(DrawArea.Width, 2));
            float strenthFromDistance = (areaCrossDistance - distance) / areaCrossDistance;
            float XStreath = (float)Math.Cos(radAngle) * strenthFromDistance * strenth;
            float YStreath = (float)Math.Sin(radAngle) * strenthFromDistance * strenth;
            shape.StepX += XStreath;
            shape.StepY += YStreath;

        }
    }
    protected override void Mouse_Click(object sender, MouseEventArgs e)
    {
        Explotion(e.X, e.Y, 8F, 25f, Color.Red, 2F);
    }
    public void SpeedDecay(float strenth)
    {
        foreach (Shape shape in shapes)
        {
            shape.StepX = shape.StepX * (1 - strenth);
            shape.StepY = shape.StepY * (1 - strenth);
        }
    }
}
