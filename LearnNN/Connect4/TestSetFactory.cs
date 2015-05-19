using HumanConnect4.Connect4.TestSets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.Connect4
{
    public class TestSetFactory
    {
        public static AbstractTestSet Create<Type>()
           where Type : AbstractTestSet, new()
        {
            return new Type();
        }
    }
}
