using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DataGenerator
{
    static class VelengHelper
    {
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

        static String GetVelengWorkingDirectory()
        {
            var path = Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            while (!Path.GetFileName(path).Equals("DataGenerator"))
            {
                path = Path.GetDirectoryName(path);
            }
            path = Path.GetDirectoryName(path);

            return path.Substring(6) + "\\Veleng\\Release\\";
        }

        public static String RunVelengParallel(String input, int threads)
        {
            String[] inputs = DevideInput(input, threads);
            Thread[] worker = new Thread[threads];
            String[] data = new String[threads];

            for (int i = 0; i < threads; i++)
            {
                worker[i] = new Thread((id) =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    Process p = new Process();
                    p.StartInfo.FileName = GetVelengWorkingDirectory() + "Veleng.exe";
                    p.StartInfo.CreateNoWindow = false;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.WorkingDirectory = GetVelengWorkingDirectory();
                    p.Start();

                    StreamWriter writer = p.StandardInput;
                    writer.Write(inputs[(int)id] + "q" + Environment.NewLine);
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
