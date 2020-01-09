using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
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

        public void WriteLocationFile(string filePath, List<CsipLocation> locations)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(locations);
            }
        }

        public List<CsipLocation> ReadLocationFile(string filePath)
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
    }
}
