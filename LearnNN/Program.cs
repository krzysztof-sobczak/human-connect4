using HumanConnect4.Connect4;
using System;
using System.Diagnostics;

namespace LearnNN
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            
            AI AI = new AI();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Learnt in " + elapsedMs.ToString() + "ms");
            
            Console.ReadLine();
        }
    }
}
