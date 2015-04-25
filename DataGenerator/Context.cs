
namespace DataGenerator
{
    class Context
    {
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
