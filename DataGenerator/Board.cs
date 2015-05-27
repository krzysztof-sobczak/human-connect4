using System;
using System.Collections.Generic;

namespace DataGenerator
{
    public class Board
    {
        DiskColor[,] _board;
        int[] _colSize;
        int _currentPlayer;
        public List<int> bestMove;

        // pamiętajmy, że (0,0) jest w lewym dolnym rogu...
        public int[][] FrameToDelta = new int[][] {
            new int [] {0, 2 }, 
            new int [] {2, 2 }, 
            new int [] {0, 0 }, 
            new int [] {2, 0 }
        };

        public int[] ColSize
        {
            get { return _colSize; }
            set { _colSize = value; }
        }

        public enum FramePosition
        {
            TOP_LEFT,
            TOP_RIGTH,
            DOWN_LEFT,
            DOWN_RIGTH
        }

        public DiskColor this[FramePosition frame, int x, int y]
        {
            get
            {
                if (x < 0 || 4 < x || y < 0 || 3 < y) throw new ArgumentException();

                int deltaX = FrameToDelta[(int)frame][0];
                int deltaY = FrameToDelta[(int)frame][1];

                return _board[x + deltaX, y + deltaY];
            }
            /*set
            {
                // value
            }*/
        }

        public enum DiskColor
        {
            ACT = 1,
            EMPTY = 0,
            PAS = -1
        }

        public Board()
        {
            _board = new DiskColor[7, 6];
            _colSize = new int[7];
            _currentPlayer = 1;     // always first
            bestMove = new List<int>();
        }

        public void MakeMove(int col)
        {
            // col = <0, 6>
            if (col < 0 || col > 6) throw new ArgumentException("You can move only from 0 to 6");
            // _col_size[] = <0, 5>
            if (_colSize[col] < 0 || _colSize[col] > 5) throw new ArgumentException("Column is full");

            _board[col, _colSize[col]++] = (DiskColor)_currentPlayer;
            ChangePlayer();
        }

        public void ChangePlayer()
        {
            _currentPlayer = -_currentPlayer;
        }

        public void MakeMoves(int[] cols)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                MakeMove(cols[i]);
            }
        }


        public override string ToString()
        {
            String s = "";
            for (int j = _board.GetLength(1) - 1; j > -1; --j)   // y
            {
                for (int i = 0; i < _board.GetLength(0); ++i)   // x
                {
                    if (_board[i, j] == 0)
                    {
                        s += " ";
                    }
                    else if (_board[i, j] > 0)
                    {
                        s += "+";
                    }
                    else
                    {
                        s += "-";
                    }
                }
                s += "\n";
            }

            return s;
        }
    }
}
