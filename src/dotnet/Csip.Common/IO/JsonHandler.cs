using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO
{
    // TODO: Combine ReadWeppResponseV3_1Files and ReadWepsResponseV5_2Files using generics and interface for service
    public class JsonHandler
    {
        public List<WeppResponseV3_1> ReadWeppResponseV3_1Files(
            string filePath,
            WeppV3_1 service)
        {
            string[] files = Directory.GetFiles(filePath, "*.json");

            List<WeppResponseV3_1> results = new List<WeppResponseV3_1>();

            foreach(var file in files)
            {
                string json = File.ReadAllText(file);
                WeppResponseV3_1 result = service.ParseResultsJson(json);

                results.Add(result);
            }

            return results;
        }

        public List<WepsResponseV5_2> ReadWepsResponseV5_2Files(
            string filePath,
            WepsV5_2 service)
        {
            string[] files = Directory.GetFiles(filePath, "*.json");

            List<WepsResponseV5_2> results = new List<WepsResponseV5_2>();

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                WepsResponseV5_2 result = service.ParseResultsJson(json);

                results.Add(result);
            }

            return results;
        }
    }
}
