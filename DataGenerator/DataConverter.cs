using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

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
            Board[] situations = new Board[lines.Length / 2];
            for (int i = 0; i < situations.Length; i++)
            {
                if (lines[i * 2 + 1].Length < 4)
                {
                    Board b = new Board();
                    b.MakeMoves(DataConverter.StringToMoves(lines[i * 2]));
                    b.bestMove = lines[i * 2 + 1][0] - '0';
                    situations[i] = b;
                }
            }

            return situations;
        }

    }
}
