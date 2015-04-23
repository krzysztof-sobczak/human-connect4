using System;
using System.Collections.Generic;

namespace DataGenerator
{
    class DataGenerator
    {
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

    }
}
