using System;
using System.Collections.Generic;

namespace DataGenerator
{
    public static class DataConverter
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

        public static Board[] ParseData(String data)
        {
            data = data.Replace("\r\n", "\n");
            String[] lines = data.Split(Environment.NewLine.ToCharArray());
            List<Board> situations = new List<Board>();
            for (int i = 0; i < lines.Length/2; i++)
            {
                if (lines[i * 2 + 1].Length < 4)
                {
                    Board b = new Board();
                    b.MakeMoves(DataConverter.StringToMoves(lines[i * 2]));
                    b.bestMove = lines[i * 2 + 1][0] - '0';
                    situations.Add(b);
                }
            }

            return situations.ToArray();
        }

        public static String[][] DevideInput(String[] input, int threads)
        {
            String[][] inputs = new String[threads][];

            int len = input.Length / threads;
            for (int i = 0; i < threads - 1; i++)
            {
                inputs[i] = new String[len];
                Array.Copy(input, i * len, inputs[i], 0, len);
            }
            inputs[threads - 1] = new String[input.Length - len * (threads - 1)];
            Array.Copy(input, (threads - 1) * len, inputs[threads - 1], 0, 
                input.Length - len * (threads - 1));

            return inputs;
        }

        public static String[] DoubleArrayToArraySplitsByNewLine(String[][] data)
        {
            String[] ret = new String[data.GetLength(0)];
            
            for (int i = 0; i < data.GetLength(0); i++)
            {
                ret[i] = String.Join(Environment.NewLine, data[i]);
            }

            return ret;
        }

        public static String[] Shuffle(String[] input)
        {
            Random rand = new Random();
            String[] shuffled = new String[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                int r = rand.Next(input.Length);
                while (shuffled[r] != null) r = (r + 1) % input.Length;
                shuffled[r] = input[i];
            }

            return shuffled;
        }

    }
}
