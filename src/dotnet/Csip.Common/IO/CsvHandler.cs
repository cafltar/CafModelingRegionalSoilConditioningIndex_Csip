using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public void WriteCsipLocationFile(string filePath, List<CsipLocation> locations)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(locations);
            }
        }

        public void WriteRusle2ResultFile(
            string filePath, 
            List<Rusle2ResponseV5_0> rusle2Responses)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(rusle2Responses);
            }
        }

        public List<CsipLocation> ReadCsipLocationFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader))
            {
                var records =
                    csv.GetRecords<CsipLocation>().ToList();

                return records;
            }
        }

        public void WriteErosionParameters(
            string filePath,
            List<ErosionParameters> erosionParameters)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(erosionParameters);
            }
        }

        public List<ErosionParameters> ReadErosionParameters(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader))
            {
                var records =
                    csv.GetRecords<ErosionParameters>().ToList();

                return records;
            }
        }

        public void WriteExperimentalResultsParameters(
            string filePath,
            List<ExperimentalResults> experimentalResults)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(experimentalResults);
            }
        }
    }
}
