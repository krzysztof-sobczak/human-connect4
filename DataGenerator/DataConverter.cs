using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

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

        static String[] DevideInput(String input, int threads)
        {
            String[] inputs = new String[threads];

            int len = input.Length / threads;
            int start = 0;
            int end = len;
            for (int i = 0; i < threads-1; i++)
            {
                while (input[end - 1] != '\n') end++;
                inputs[i] = input.Substring(start, end - start);
                start = end;
                end += len;
            }
            inputs[threads-1] = input.Substring(start, input.Length - start);

            return inputs;
        }

        public static String RunVelengParallel(String input, int threads)
        {
            String[] inputs = DevideInput(input, threads);
            Thread[] worker = new Thread[threads];
            String[] data = new String[threads];
            //String data = "";

            for (int i = 0; i < threads; i++)
            {
                worker[i] = new Thread((id) =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    Process p = new Process();
                    p.StartInfo.FileName = "D:\\dawid\\studia\\msi2\\repo\\Veleng\\Debug\\Veleng.exe";
                    p.StartInfo.CreateNoWindow = false;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.WorkingDirectory = "D:\\dawid\\studia\\msi2\\repo\\Veleng\\Debug\\";
                    p.Start();

                    StreamWriter writer = p.StandardInput;
                    writer.Write(inputs[(int)id]);
                    writer.WriteLine("q");
                    writer.Close();

                    data[(int)id] = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                });
                worker[i].Start(i);
            }

            for (int i = 0; i < threads; i++)
            {
                worker[i].Join();
            }
            
            return String.Join("", data);
        }
    }
}
