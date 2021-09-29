﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace TowerDefence_ClientSide
{
    public partial class GameStartMenu : Form
    {
        public GameStartMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToServer(Player.PLAYER1);
        }

        private void GameStartMenu_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectToServer(Player.PLAYER2);
        }
        private void ConnectToServer(Player player)
        {
            GameWindow form = new GameWindow(player);
            form.Show();
        }
    }
}