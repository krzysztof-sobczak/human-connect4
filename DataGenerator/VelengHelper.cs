using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DataGenerator
{
    static class VelengHelper
    {
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

        public static String RunVelengParallel(String[] input, int threads)
        {
            String[] inputs = DataConverter.DoubleArrayToArraySplitsByNewLine(
                DataConverter.DevideInput(input, threads)
            );
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
                    writer.Write(inputs[(int)id] +
                        Environment.NewLine +"q" + Environment.NewLine);
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
