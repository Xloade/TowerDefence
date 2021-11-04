using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_SharedContent;

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
            if(comboBox1.SelectedItem == null)
            {
                ConnectToServer(PlayerType.PLAYER1, "NotSelected");
            }
            else
            {
                ConnectToServer(PlayerType.PLAYER1, comboBox1.SelectedItem.ToString());
            }
            
        }

        private void GameStartMenu_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectToServer(PlayerType.PLAYER2, comboBox1.SelectedItem.ToString());
        }
        private void ConnectToServer(PlayerType player, String mapType)
        {
            GameWindow form = new GameWindow(player, mapType);
            form.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
