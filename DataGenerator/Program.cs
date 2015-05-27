using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int testNumber = 0;
            string filename = "data__0_5";
            string extension = ".csv";
            int dataSize = 5;
            int threads = 64;

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

            string[] input;
            switch (testNumber)
            {
                case 0:
                    input = DataGenerator.GenerateEasyInputForVeleng(dataSize);
                    Console.WriteLine("Gererating " + dataSize + " moves on "
                        + threads + "x2 threads and save to '" + filename + "_*" + extension + "'...");
                    break;
                case 1:
                    input = DataGenerator.GenerateRandomInputForVeleng(dataSize);
                    Console.WriteLine("Gererating " + dataSize + " random records on "
                        + threads + "x2 threads and save to '" + filename + "_*" + extension + "'...");
                    break;
                default:
                    input = DataGenerator.GenerateRandomInputForVeleng(dataSize);
                    break;
            }

            // input - 1 data per line
            input = DataConverter.Shuffle(input);
            String[][] inputs = DataConverter.DevideInput(input, 2);
            //// input - 1 input data for thread per line

            String[] input4learn = inputs[0];
            String[] input4test = inputs[1];
            Thread worker4test, worker4learn;

            var watch = Stopwatch.StartNew();

            worker4learn = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                DataGenerator.GenerateData(path + filename + "_learn" + extension, input4learn, threads, false);
            });
            worker4learn.Start();

            worker4test = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                DataGenerator.GenerateData(path + filename + "_test" + extension, input4test, threads, true);
            });
            worker4test.Start();

            worker4test.Join();
            worker4learn.Join();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Generated in " + elapsedMs.ToString() + "ms");
            Console.ReadLine();
        }
    }
}
