using CsvHelper.Configuration.Attributes;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models
{
    public class InputCentroidLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        [Name("AgroecologicalClass")]
        public int Anthrome { get; set; }
    }
}
