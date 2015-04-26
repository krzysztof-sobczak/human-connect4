using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DataGenerator
{
    public class DataGenerator
    {
        Board board;

        public DataGenerator(Board board)
        {
            this.board = board;
        }

        public static String GenerateInputForVeleng1()
        {
            String input = "";
            List<String> data = new List<String>();
            int len;

            for (int j = 1; j < 8; j++)
            {
                data.Add(j.ToString());
            }

            for (int k = 0; k < 2; k++)
            {
                len = data.Count;
                for (int i = 0; i < len; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        data.Add(data[i] + j.ToString());
                    }
                }
            }

            foreach (String item in data)
            {
                input += item + '0' + Environment.NewLine;
            }
            return input;
        }

        static String[] DevideInput(String input, int threads)
        {
            String[] inputs = new String[threads];

            int len = input.Length / threads;
            int start = 0;
            int end = len;
            for (int i = 0; i < threads - 1; i++)
            {
                while (input[end - 1] != '\n') end++;
                inputs[i] = input.Substring(start, end - start);
                start = end;
                end += len;
            }
            inputs[threads - 1] = input.Substring(start, input.Length - start);

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

        public void GenerateContexts()
        {
            foreach (Board.FramePosition frame in Enum.GetValues(typeof(Board.FramePosition)))
            {
                GenerateContextsForFrame(frame);
            }
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

    }
}
