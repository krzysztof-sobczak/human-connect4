using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Console.WriteLine(input1);

            String data = DataConverter.RunVeleng(input1);
            //Console.WriteLine(data);

            File.WriteAllText(dataFilename, data);
            //Console.ReadLine();
        }
    }
}
