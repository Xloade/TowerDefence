using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    class Map
    {
        Player player1;
        Player player2;
        List<Soldier> soldiers;
        List<Tower> towers;

        public Map()
        {
            player1 = new Player();
            player2 = new Player();
            soldiers = new List<Soldier>();
            towers = new List<Tower>();
        }
    }
}
