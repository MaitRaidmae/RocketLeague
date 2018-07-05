using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeague
{
    class Utils
    {
        public static void WriteToConsole(string logLine)
        {
            string currentTime = DateTime.UtcNow.ToString();
            Console.WriteLine(currentTime + " - " + logLine);
        }
    }
}
