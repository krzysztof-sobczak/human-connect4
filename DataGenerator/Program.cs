using System;
using System.IO;

namespace Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            String dataFilename = "data.txt";
            String s = "44477746350";
            
            Board b = new Board();
            b.MakeMoves(DataConverter.StringToMoves(s));

            String input1 = DataGenerator.DataGenerator.GenerateInputForVeleng1();
            Console.WriteLine(input1);

            String data = DataConverter.RunVelengParallel(input1, 8);
            Console.WriteLine(data);

            File.WriteAllText(dataFilename, data);
            Console.ReadLine();
        }
    }
}
