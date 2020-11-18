using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor)); //index 4 is red, 3, 9 are blue beautiful. I like 11
            Console.ForegroundColor = colors[11];
            Simulation simulation = new Simulation();
            simulation.Simulate();
        }
    }
}
