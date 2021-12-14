using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TowerDefence_SharedContent
{
    static public class MyConsole
    {
        private static List<string> _writeOnce = new List<string>();
        private static Dictionary<string, int> _writeWithCount = new Dictionary<string, int>();
        static private Timer _timer = new Timer();
        static private Dictionary<string, List<int>> _randomIds = new Dictionary<string, List<int>>();
        static Random _rnd = new Random();
        static MyConsole()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateConsole();
        }

        public static void WriteLineOnce(string message)
        {
            lock (_writeOnce)
            {
                if(!_writeOnce.Contains(message))
                {
                    _writeOnce.Add(message);
                }
            }
        }
        public static void WriteLineWithCount(string message)
        {
            lock (_writeWithCount)
            {
                if (_writeWithCount.ContainsKey(message))
                {
                    _writeWithCount[message]++;
                }
                else
                {
                    _writeWithCount.Add(message, 1);
                }
            }
        }
        private static void UpdateConsole()
        {
            Console.Clear();
            lock (_writeWithCount)
            {
                foreach (KeyValuePair<string, int> kvp in _writeWithCount)
                {
                    Console.WriteLine($"{kvp.Key} ({kvp.Value})");
                }
            }
            Console.WriteLine("-----------------------------------------");
            lock (_writeOnce)
            {
                foreach (string message in _writeOnce)
                {
                    Console.WriteLine(message);
                }
            }
        }
        static public bool LookForMultiThreads(string message, int randomId)
        {
            lock (_randomIds)
            {
                if (!_randomIds.ContainsKey(message))
                {
                    _randomIds.Add(message, new List<int>());
                }
                List<int> currentRandIds = _randomIds[message];
                if (currentRandIds.Contains(randomId))
                {
                    if (currentRandIds[currentRandIds.Count - 1] != randomId)
                    {
                        WriteLineWithCount($"\"{message}\" in multiple threads detected");
                        return true;
                    }
                    currentRandIds.Remove(randomId);
                }
                else
                {
                    currentRandIds.Add(randomId);
                }
            }
            return false;
        }
        public static int Random()
        {
            lock (_rnd)
            {
                return _rnd.Next();
            }
        }
    }
}
