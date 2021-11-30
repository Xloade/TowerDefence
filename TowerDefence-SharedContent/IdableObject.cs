using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class IdableObject
    {
        private static long IdCounter = 0;

        public IdableObject()
        {
            Id = IdCounter;
            IdCounter++;
        }
        public long Id {get; set; }
    }
}
