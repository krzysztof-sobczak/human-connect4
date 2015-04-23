using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    static class DataConverter
    {
        /// <summary>
        /// Konwersja ciągu znaków, odpowiadająca ruchom z zakresu 1-7, 
        /// do tablicy liczb z ruchami z zakresu 0-6.
        /// </summary>
        /// <param name="sMoves"></param>
        /// <returns></returns>
        public static int[] StringToMoves(String sMoves)
        {
            int[] iMoves;
            
            if (sMoves[sMoves.Length-1] == '0') {
                iMoves = new int[sMoves.Length-1];
            } else {
                iMoves = new int[sMoves.Length];
            }

            for (int i = 0; i < iMoves.Length; i++)
            {
                if (sMoves[i] > '7' || sMoves[i] < '1') throw new ArgumentException();
                iMoves[i] = sMoves[i] - '0' - 1;
            }

            return iMoves;
        }

        public static Board MovesToBoard(int[] moves)
        {
            Board board = new Board();
            board.MakeMoves(moves);

            return board;
        }

        public static String RunVeleng(String input)
        {
            Process p = new Process();
            p.StartInfo.FileName = "D:\\dawid\\studia\\msi2\\repo\\Veleng\\Debug\\Veleng.exe";
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WorkingDirectory = "D:\\dawid\\studia\\msi2\\repo\\Veleng\\Debug\\";
            p.Start();

            StreamWriter writer = p.StandardInput;
            writer.Write(input);
            writer.WriteLine("q");
            writer.Close();

            String data = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return data;
        }
    }
}
