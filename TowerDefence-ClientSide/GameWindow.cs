using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Console;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_SharedContent;

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
    Map map;

    public GameWindow(Player playerType) : base(Color.Cyan, playerType.ToString(),
        1000, 700, "Buy soldier")
    {
        this.playerType = playerType;
        startSignalR();
    }
    
    private void updateMap(Map map)
    {
        this.map = map;
        shapes = new List<Shape>();
        map.GetPlayer(PlayerType.PLAYER1).soldiers.ForEach((soldier) =>
        {
            shapes.Add(new Shape(soldier.Coordinates, 100,100, Image.FromFile(@"../../../Sprites/soldier(Blue).png")));
        });
        map.GetPlayer(PlayerType.PLAYER2).soldiers.ForEach((soldier) =>
        {
            shapes.Add(new Shape(soldier.Coordinates, 100, 100, Image.FromFile(@"../../../Sprites/soldier(Red).png")));
        });
        Refresh();
    }

    private void startSignalR()
    {
        connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/GameHub")
                .Build();
        connection.On<string>("ReceiveMessage", (updatedMapJson) =>
        {
            Map updatedMap = JsonConvert.DeserializeObject<Map>(updatedMapJson);
            updateMap(updatedMap);
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
