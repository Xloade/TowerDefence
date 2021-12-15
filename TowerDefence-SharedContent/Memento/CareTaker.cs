using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Memento
{
    public class CareTaker
    {
        public List<MementoPlayer> soldierCurrencyList;

        public CareTaker()
        {
            soldierCurrencyList = new List<MementoPlayer>();
        }

        public MementoPlayer GetMemento(int index)
        {
            MementoPlayer restoreSoldierCurrency = soldierCurrencyList[index];
            soldierCurrencyList.RemoveAt(index);
            return restoreSoldierCurrency;
        }
    }
}
