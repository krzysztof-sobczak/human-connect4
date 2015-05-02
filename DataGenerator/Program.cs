using System;
using System.Diagnostics;
using System.IO;

namespace DataGenerator
{
    class Program
    {
        static void Main()
        {
            string s = DataGenerator.GenerateRandomInputForVeleng(100);
            var path = Path.GetDirectoryName(
                Path.GetDirectoryName(Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
                )).Substring(6);

            var watch = Stopwatch.StartNew();
            DataGenerator.GenerateData(path + "\\data5.csv", 
                DataGenerator.GenerateRandomInputForVeleng(1500), 8);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Generated in " + elapsedMs.ToString() + "ms");
            Console.ReadLine();
        }
    }
}
