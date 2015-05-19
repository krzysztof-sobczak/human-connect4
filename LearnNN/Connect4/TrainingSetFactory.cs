using HumanConnect4.Connect4.TrainingSets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.Connect4
{
    public class TrainingSetFactory
    {
        public static AbstractTrainingSet Create<Type>()
           where Type : AbstractTrainingSet, new()
        {
            return new Type();
        }
    }
}
