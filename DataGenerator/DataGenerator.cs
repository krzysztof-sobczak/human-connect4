using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataGenerator
{
    public class DataGenerator
    {
        private static List<string> contextColumns =
            new List<string> { "frame1_context1","frame1_context2","frame1_context3",
            "frame1_context4","frame1_context5","frame1_context6","frame1_context7",
            "frame1_context8","frame1_context9","frame1_context10","frame1_context11",
            "frame1_context12","frame1_context13","frame1_context14","frame1_context15",
            "frame1_context16","frame1_context17","frame2_context1","frame2_context2",
            "frame2_context3","frame2_context4","frame2_context5","frame2_context6",
            "frame2_context7","frame2_context8","frame2_context9","frame2_context10",
            "frame2_context11","frame2_context12","frame2_context13","frame2_context14",
            "frame2_context15","frame2_context16","frame2_context17","frame3_context1",
            "frame3_context2","frame3_context3","frame3_context4","frame3_context5",
            "frame3_context6","frame3_context7","frame3_context8","frame3_context9",
            "frame3_context10","frame3_context11","frame3_context12","frame3_context13",
            "frame3_context14","frame3_context15","frame3_context16","frame3_context17",
            "frame4_context1","frame4_context2","frame4_context3","frame4_context4",
            "frame4_context5","frame4_context6","frame4_context7","frame4_context8",
            "frame4_context9","frame4_context10","frame4_context11","frame4_context12",
            "frame4_context13","frame4_context14","frame4_context15","frame4_context16",
            "frame4_context17", "best_column" };

        public Board board { get; set; }

        public DataGenerator(Board board)
        {
            this.board = board;
        }

        public static String[] GenerateEasyInputForVeleng(int dataSize)
        {
            //String input = "";
            List<String> data = new List<String>();
            List<String> prevlastLevel = new List<String>();
            List<String> lastLevel = new List<String>();
            //int len;

            // generate first move
            for (int j = 1; j < 8; j++)
            {
                lastLevel.Add(j.ToString());
            }

            dataSize -= 1;
            // generate rest moves
            // k = k-th move
            for (int k = 0; k < dataSize; k++)
            {
                data.AddRange(prevlastLevel);
                prevlastLevel = lastLevel;
                lastLevel = new List<string>();
                foreach(var item in prevlastLevel)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        lastLevel.Add(item + j.ToString());
                    }
                }
            }
            data.AddRange(prevlastLevel);
            data.AddRange(lastLevel);

            for (int i = 0; i < data.Count; i++)
			{
                data[i] = data[i] + "0" + Environment.NewLine;
			}

            return data.ToArray();
        }

        public static String[] GenerateRandomInputForVeleng(int boardsCount)
        {
            const int minMovesCount = 6;
            //const int maxMovesCount = 6 * 7 - 10;
            const int maxMovesCount = 12;

            int len = 0;
            Random rand = new Random();

            if (boardsCount < 1) boardsCount = rand.Next();
            String[] input = new string[boardsCount];

            for (int i = 0; i < boardsCount; i++)
            {
                int movesCount = minMovesCount + rand.Next(
                    maxMovesCount - minMovesCount + 1
                );
                string move = "";
                for (int j = 0; j < movesCount; j++)
                {
                    move += (rand.Next(7) + 1).ToString();
                }
                input[++len] = move + '0' + Environment.NewLine;
            }
            
            return input;
        }

        public Context[][] GenerateContexts()
        {
            Context[][] conts = new Context[Enum.GetValues(typeof(Board.FramePosition)).Length][];
            int i = 0;
            foreach (Board.FramePosition frame in Enum.GetValues(typeof(Board.FramePosition)))
            {
                conts[i++] = GenerateContextsForFrame(frame);
            }

            return conts;
        }

        public Context[] GenerateContextsForFrame(Board.FramePosition frame)
        {
            int[][][] contextsLines = GenerateContextsLinesParams();
            int[,] crossingLinePerField = new int[5, 4];
            List<Context> contexts = new List<Context>();

            for (int contextsLineIndex = 0; contextsLineIndex < contextsLines.GetLength(0); contextsLineIndex++)
            {
                Context context = new Context();

                // [F2]
                context.contextType = (Context.ContextType) contextsLines[contextsLineIndex][2][0];

                // [F3]
                context.row = contextsLines[contextsLineIndex][0][1];

                // [F4]
                context.deep = 0;

                // [A2]
                context.missingDisk = 0;

                int[] diskPerPlayerCount = new int[3] {0, 0, 0};

                int xInFrame = contextsLines[contextsLineIndex][0][0];
                int yInFrame = contextsLines[contextsLineIndex][0][1];

                // for each field in context
                for (int i = 0; i < 4; i++)
                {
                    // copy value of field (~[F1])
                    context.values[i] = board[frame, xInFrame, yInFrame];

                    // [F4]
                    context.deep += NonNegative(
                        yInFrame - NonNegative(
                            board.ColSize[xInFrame + board.FrameToDelta[(int)frame][0]] 
                                - board.FrameToDelta[(int)frame][1]
                        ) + 1
                    );

                    // [A1]
                    diskPerPlayerCount[1 + (int)context.values[i]]++;

                    // [A2]
                    if (context.values[i] == Board.DiskColor.EMPTY) context.missingDisk++;

                    // upgrade position
                    xInFrame = xInFrame + contextsLines[contextsLineIndex][1][0];
                    yInFrame = yInFrame + contextsLines[contextsLineIndex][1][1];
                }

                // [F4]
                if (context.contextType == Context.ContextType.VERTICAL)
                {
                    // last position
                    xInFrame = xInFrame - contextsLines[contextsLineIndex][1][0];
                    yInFrame = yInFrame - contextsLines[contextsLineIndex][1][1];

                    // take only the higthest disk
                    context.deep = NonNegative(
                        yInFrame - NonNegative(
                            board.ColSize[xInFrame] - board.FrameToDelta[(int)frame][1]
                        ) + 1
                    );
                }
                
                // [A1]
                int ifACT = Convert.ToInt32(diskPerPlayerCount[1 + (int)Board.DiskColor.ACT] > 0);
                int ifPAS = Convert.ToInt32(diskPerPlayerCount[1 + (int)Board.DiskColor.PAS] > 0);
                context.whoCanWin = (Board.DiskColor)(ifACT - ifPAS);

                // [A3/1]
                // Jeśli ten kontekst jest 'dobry'
                // to zwiększamy licznik w każdym polu zawierającego się w tym kontekście.
                // Na końcu licznik będzie oznaczał 
                //  ile jest 'dobrych' kontekstów przechodzących przez to pole.
                //
                // metoda po to, żeby było łatwo zmienić warunki bycia 'dobrym'
                if (IfItsGoodContext(context))
                {
                    xInFrame = contextsLines[contextsLineIndex][0][0];
                    yInFrame = contextsLines[contextsLineIndex][0][1];

                    for (int i = 0; i < 4; i++)
                    {
                        crossingLinePerField[xInFrame, yInFrame]++;

                        // upgrade position
                        xInFrame = xInFrame + contextsLines[contextsLineIndex][1][0];
                        yInFrame = yInFrame + contextsLines[contextsLineIndex][1][1];
                    }
                }

                // add to list
                contexts.Add(context);
            }

            Context[] contextToReturn = contexts.ToArray();

            // [A3/2]
            // dla każdego pola w kontekście zliczamy 
            //  ile kontekstów przez niego przechodzi
            for (int contextsLineIndex = 0; contextsLineIndex < contextsLines.GetLength(0); contextsLineIndex++)
            {
                int xInFrame = contextsLines[contextsLineIndex][0][0];
                int yInFrame = contextsLines[contextsLineIndex][0][1];

                for (int i = 0; i < 4; i++)
                {
                    contextToReturn[contextsLineIndex].crossingOpenedLines 
                        += crossingLinePerField[xInFrame, yInFrame];

                    /*if (IfItsGoodContext(contextToReturn[contextsLineIndex]))
                    {
                        contextToReturn[contextsLineIndex].crossingOpenedLines -= 4;
                    }*/

                    // upgrade position
                    xInFrame = xInFrame + contextsLines[contextsLineIndex][1][0];
                    yInFrame = yInFrame + contextsLines[contextsLineIndex][1][1];
                }
            }

            return contextToReturn;
        }

        private bool IfItsGoodContext(Context context)
        {
            if (context.whoCanWin == 0) return false;
            if (context.missingDisk > 3) return false;
            return true;
        }

        static int[][][] GenerateContextsLinesParams()
        {
            // tworzę tablicę z parametrami linii,
            // które opisują kontekst
            // parametry = 
            //  punkt początkowy;
            //  delta (dx,dy), o którą należy się przesuwać do następnych pól => kierunek
            int[][][] contextsLines = new int[17][][];
            int contextsLinesCount = 0;

            // -
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    contextsLines[contextsLinesCount++]
                        = new int[][] { 
                            new int[] { j, i },
                            new int[] { 1, 0 },
                            new int[] { (int)Context.ContextType.HORIZONTAL }
                        };
                }
            }

            // |
            for (int i = 0; i < 5; i++)
            {
                contextsLines[contextsLinesCount++]
                    = new int[][] {
                        new int[] { i, 0 },
                        new int[] { 0, 1 },
                        new int[] { (int)Context.ContextType.VERTICAL }
                    };
            }

            // /
            for (int i = 0; i < 2; i++)
            {
                contextsLines[contextsLinesCount++]
                    = new int[][] {
                        new int[] { i, 0 },
                        new int[] { 1, 1 },
                        new int[] { (int)Context.ContextType.SLASH }
                    };
            }

            // \
            for (int i = 0; i < 2; i++)
            {
                contextsLines[contextsLinesCount++]
                    = new int[][] {
                        new int[] { 3 + i, 0 },
                        new int[] { -1, 1 },
                        new int[] { (int)Context.ContextType.BACKSLASH }
                    };
            }

            return contextsLines;
        }


        public static int NonNegative(int x)
        {
            if (x > 0) return x;
            else return 0;
        }

        public static void GenerateData(String path, String[] input, int threadsCount, bool isTesting)
        {
            //Console.WriteLine(input);

            String boardsS = VelengHelper.RunVelengParallel(input, threadsCount);
            Board[] boards = null;
            if (isTesting)
            {
                boards = DataConverter.ParseDataForTesting(boardsS);
            }
            else
            {
                boards = DataConverter.ParseDataForLearning(boardsS);
            }

            using (var stream = new StreamWriter(path))
            {
                using (var writer = new CsvWriter(stream))
                {
                    // column name
                    foreach (var contName in contextColumns)
                    {
                        writer.WriteField(contName);
                    }
                    writer.NextRecord();

                    // data
                    for (int boardsIndex = 0; boardsIndex < boards.Length; boardsIndex++)
                    {
                        DataGenerator gen = new DataGenerator(boards[boardsIndex]);
                        Context[][] conts = gen.GenerateContexts();

                        for (int i = 0; i < conts.Length; i++)
                        {
                            for (int j = 0; j < conts[i].Length; j++)
                            {
                                writer.WriteField(conts[i][j].ToString(), true);
                            }
                        }
                        if (gen.board.bestMove.Count == 1)
                        {
                            writer.WriteField(gen.board.bestMove[0]);
                        }
                        else
                        {
                            String s = "{";
                            foreach (var item in gen.board.bestMove)
                            {
                                s += item.ToString() + ",";
                            }
                            s = s.Remove(s.Length - 1);
                            s += "}";
                            writer.WriteField(s);
                        }
                        writer.NextRecord();
                    }
                }
            }
        }

    }
}
