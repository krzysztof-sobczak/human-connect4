using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    class Board
    {
        int[,] _board;
        int[] _col_size;
        int _currentPlayer;

        public Board()
        {
            _board = new int[7, 6];
            _col_size = new int[7];
            _currentPlayer = 1;     // always first
        }

        public void MakeMove(int col)
        {
            // col = <0, 6>
            if (col < 0 || col > 6) throw new ArgumentException("You can move only from 0 to 6");
            // _col_size[] = <0, 5>
            if (_col_size[col] < 0 || _col_size[col] > 5) throw new ArgumentException("Column is full");

            _board[col, _col_size[col]++] = _currentPlayer;
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
