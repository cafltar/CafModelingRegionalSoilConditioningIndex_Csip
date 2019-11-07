﻿using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO
{
    public class CsvHandler
    {
        public List<FlexCroppingLocation> ReadFlexCroppingLocationFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader))
            {
                var records = 
                    csv.GetRecords<FlexCroppingLocation>().ToList();

                return records;
            }
        }

        public void WriteLocationFile(string filePath, List<Location> locations)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(locations);
            }
        }

        public List<Location> ReadLocationFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader))
            {
                var records =
                    csv.GetRecords<Location>().ToList();

                return records;
            }
        }
    }
}
