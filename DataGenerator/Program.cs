using System;
using System.Diagnostics;
using System.IO;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int testNumber = 0;
            string filename = "data9.csv";
            int dataSize = 4;
            int threads = 8;

            if (args.Length >= 1)
            {
                int.TryParse(args[0], out testNumber);
                
                if (args.Length >= 2)
                {
                    filename = args[1];

                    if (args.Length >= 3)
                    {
                        int.TryParse(args[2], out dataSize);

                        if (args.Length >= 4)
                        {
                            int.TryParse(args[3], out threads);
                        }
                    }
                }
            }
            var path = Path.GetDirectoryName(
                Path.GetDirectoryName(Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
                )).Substring(6) + "\\";

            string input = "";
            switch (testNumber)
            {
                case 0:
                    input = DataGenerator.GenerateEasyInputForVeleng(dataSize);
                    Console.WriteLine("Gererating " + dataSize + " moves on "
                        + threads + " threads and save to '" + filename + "'...");
                    break;
                case 1:
                    input = DataGenerator.GenerateRandomInputForVeleng(dataSize);
                    Console.WriteLine("Gererating " + dataSize + " random records on "
                        + threads + " threads and save to '" + filename + "'...");
                    break;
                default:
                    input = DataGenerator.GenerateRandomInputForVeleng(dataSize);
                    break;
            }

            var watch = Stopwatch.StartNew();
            DataGenerator.GenerateData(path + filename, input, threads);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Generated in " + elapsedMs.ToString() + "ms");
            Console.ReadLine();
        }
    }
}
