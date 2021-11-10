using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace TowerDefence_SharedContent
{
    static public class MyConsole
    {
        private static List<string> writeOnce = new List<string>();
        private static Dictionary<string, int> writeWithCount = new Dictionary<string, int>();
        static private Timer timer = new Timer();
        static private Dictionary<string, List<int>> randomIds = new Dictionary<string, List<int>>();
        static Random rnd = new Random();
        static MyConsole()
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateConsole();
        }

        public static void WriteLineOnce(string message)
        {
            lock (writeOnce)
            {
                if(!writeOnce.Contains(message))
                {
                    writeOnce.Add(message);
                }
            }
        }
        public static void WriteLineWithCount(string message)
        {
            lock (writeWithCount)
            {
                if (writeWithCount.ContainsKey(message))
                {
                    writeWithCount[message]++;
                }
                else
                {
                    writeWithCount.Add(message, 1);
                }
            }
        }
        private static void UpdateConsole()
        {
            Console.Clear();
            lock (writeWithCount)
            {
                foreach (KeyValuePair<string, int> kvp in writeWithCount)
                {
                    Console.WriteLine($"{kvp.Key} ({kvp.Value})");
                }
            }
            Console.WriteLine("-----------------------------------------");
            lock (writeOnce)
            {
                foreach (string message in writeOnce)
                {
                    Console.WriteLine(message);
                }
            }
        }
        static public bool LookForMultiThreads(string message, int randomId)
        {
            lock (randomIds)
            {
                if (!randomIds.ContainsKey(message))
                {
                    randomIds.Add(message, new List<int>());
                }
                List<int> currentRandIds = randomIds[message];
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
            lock (rnd)
            {
                return rnd.Next();
            }
        }
    }
}
