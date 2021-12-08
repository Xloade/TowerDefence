using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace TowerDefence_ClientSide.Composite
{
    public partial class PlatoonControl : UserControl
    {
        private readonly HubConnection HubConnection;
        private readonly MapUpdater MapUpdater;
        public PlatoonControl(HubConnection hubConnection, MapUpdater mapUpdater)
        {
            InitializeComponent();
            HubConnection = hubConnection;
            MapUpdater = mapUpdater;

            this.Location = new Point(0, 640);
        }
    }
}
