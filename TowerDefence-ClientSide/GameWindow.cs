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

class GameWindow : Window
{
    List<Shape> shapes = new List<Shape>();

    HubConnection connection;
    Player playerType;
    public GameWindow(Player playerType) : base(Color.Cyan, playerType.ToString(),
        1000, 700, "Buy soldier")
    {
        this.playerType = playerType;
        startSignalR();
    }
    private void startSignalR()
    {
        connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/GameHub")
                .Build();
        connection.On<string, string, List<String>>("ReceiveMessage", (user, function, args) =>
        {
            Graphics gr = Graphics.FromImage(DrawArea);
            switch (function.ToLower())
            {
                case "clear":
                    ClearArea();
                    break;
                default:
                    break;
            }
            Refresh();
        });
        connection.StartAsync();
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
        //gr.Dispose();
    }

    protected override void btn_Click(object sender, EventArgs e)
    {
        switch (((Button)sender).Name)
        {
            case "Buy soldier":
                connection.SendAsync("buySoldier", playerType);
                break;
            default:
                break;
        }
        // connection.SendAsync("SendMessage", playerType.ToString(), (sender as Button).Name, new string[] { });
    }
 
    protected override void Mouse_Click(object sender, MouseEventArgs e)
    {
        connection.SendAsync("SendMessage", playerType.ToString(), "explotion", new string[] { e.X.ToString(), e.Y.ToString() });
    }
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        graphicalTimer.Stop();
        connection.StopAsync();
        base.OnFormClosing(e);
    }
}
