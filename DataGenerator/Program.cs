using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            String s = "44477746350";
            
            Board b = new Board();
            b.MakeMoves(DataConverter.StringToMoves(s));
            Console.Write(b.ToString());

            Console.ReadLine();
        }
    }
}
