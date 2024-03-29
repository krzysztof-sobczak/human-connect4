﻿using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HumanConnect4.Connect4.TrainingSets
{
    public class TrainingSetCsvMap : CsvClassMap<TrainingSetCsv>
    {

        private List<string> contextColumns =
        new List<string> { "frame1_context1","frame1_context2","frame1_context3","frame1_context4","frame1_context5","frame1_context6","frame1_context7","frame1_context8","frame1_context9","frame1_context10","frame1_context11","frame1_context12","frame1_context13","frame1_context14","frame1_context15","frame1_context16","frame1_context17","frame2_context1","frame2_context2","frame2_context3","frame2_context4","frame2_context5","frame2_context6","frame2_context7","frame2_context8","frame2_context9","frame2_context10","frame2_context11","frame2_context12","frame2_context13","frame2_context14","frame2_context15","frame2_context16","frame2_context17","frame3_context1","frame3_context2","frame3_context3","frame3_context4","frame3_context5","frame3_context6","frame3_context7","frame3_context8","frame3_context9","frame3_context10","frame3_context11","frame3_context12","frame3_context13","frame3_context14","frame3_context15","frame3_context16","frame3_context17","frame4_context1","frame4_context2","frame4_context3","frame4_context4","frame4_context5","frame4_context6","frame4_context7","frame4_context8","frame4_context9","frame4_context10","frame4_context11","frame4_context12","frame4_context13","frame4_context14","frame4_context15","frame4_context16","frame4_context17" };

        public override void CreateMap()
        {
            Map(m => m.Contexts).ConvertUsing(row =>
                contextColumns
                    .Select(column => row.GetField<string>(column))
                    .Where(value => String.IsNullOrWhiteSpace(value) == false).ToList()
                );
            Map(m => m.BestColumn).Name("best_column").Index(68);
        }
    }
}
