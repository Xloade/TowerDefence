using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using TowerDefence_ClientSide.Proxy;

namespace TowerDefence_ClientSide.Composite
{
    public partial class PlatoonControl : UserControl
    {
        private readonly ServerConnection HubConnection;
        private readonly MapUpdater MapUpdater;
        public PlatoonControl(ServerConnection hubConnection, MapUpdater mapUpdater)
        {
            InitializeComponent();
            HubConnection = hubConnection;
            MapUpdater = mapUpdater;

            this.Location = new Point(0, 470);
        }

        private void deselectOneButton_Click(object sender, EventArgs e)
        {
            MapUpdater.RemoveOneSelection();
        }

        private void DeselectAllButton_Click(object sender, EventArgs e)
        {
            MapUpdater.RemoveAllSelection();
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            MapUpdater.SelectAll();
        }
    }
}
