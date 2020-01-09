namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files
{
    /// <summary>
    /// A combination of values from WEPP and WEPS responses as needed for calculating soil conditioning index using the SCI service
    /// </summary>
    public class ErosionParameters
    {
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public string RotationName { get; set; }

        public double WaterOM { get; set; }

        public double WindOM { get; set; }

        public double WaterFO { get; set; }

        public double WindFO { get; set; }

        public double WaterErosion { get; set; }

        public double WindErosion { get; set; }
    }
}
