using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent
{
    public class IdableObject
    {
        private static long _idCounter = 0;

        public IdableObject()
        {
            Id = _idCounter;
            _idCounter++;
        }
        public long Id {get; set; }
    }
}
