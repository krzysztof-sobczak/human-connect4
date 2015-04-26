
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
    }
}
