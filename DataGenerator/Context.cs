
namespace DataGenerator
{
    public class Context
    {
        public Context () {	}

        public Context(Board.DiskColor[] values, ContextType contextType, 
            int row, int deep, Board.DiskColor whoCanWin, 
            int missingDisk, int crossingOpenedLines)
        {
            this.values = values;
            this.contextType = contextType;
            this.row = row;
            this.deep = deep;
            this.whoCanWin = whoCanWin;
            this.missingDisk = missingDisk;
            this.crossingOpenedLines = crossingOpenedLines;
        }

        public enum ContextType
        {
            VERTICAL,
            HORIZONTAL,
            SLASH,
            BACKSLASH
        }


        public Board.DiskColor[] values = new Board.DiskColor[4];

        // [F2]
        public ContextType contextType;
        // [F3]
        public int row;
        // [F4]
        public int deep;

        // [A1]
        public Board.DiskColor whoCanWin;
        // [A2]
        public int missingDisk;
        // [A3]
        public int crossingOpenedLines;


        public override string ToString()
        {
            string s = "{";
            for (int i = 0; i < values.Length; i++)
			{
                s += ((int)values[i]).ToString() + ",";
			}
            s += ((int)contextType).ToString() + ",";
            s += row.ToString() + ",";
            s += deep.ToString() + ",";
            s += ((int)whoCanWin).ToString() + ",";
            s += missingDisk.ToString() + ",";
            s += crossingOpenedLines.ToString() + "}";
            return s;
        }

    }
}
