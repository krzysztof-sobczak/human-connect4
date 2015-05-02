using System;
using System.IO;

namespace DataGenerator
{
    class Program
    {
        static String dataFilename = "data.txt";

        // generate data
        /*static void Main(string[] args)
        {
            String s = "44477746350";
            
            Board b = new Board();
            b.MakeMoves(DataConverter.StringToMoves(s));

            String input1 = DataGenerator.DataGenerator.GenerateInputForVeleng1();
            Console.WriteLine(input1);

            String data = DataConverter.RunVelengParallel(input1, 8);
            Console.WriteLine(data);

            File.WriteAllText(dataFilename, data);
            Console.ReadLine();
        }*/

        // load & parse data
        /*static void Main(string[] args)
        {
            String dataFromFile = File.ReadAllText(dataFilename);
            Board[] data = DataConverter.DevideData(dataFromFile);
            
            DataGenerator gen = new DataGenerator(data[data.Length-1]);
            gen.GenerateContextsForFrame(Board.FramePosition.DOWN_LEFT);
        }*/

        static void Main()
        {
            var path = Path.GetDirectoryName(
                Path.GetDirectoryName(Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
                )).Substring(6);

            DataGenerator.GenerateData(path + "\\data.csv");
        }
    }
}
