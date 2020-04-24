namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files
{
    /// <summary>
    /// A combination of values from WEPP and WEPS responses as needed for calculating soil conditioning index using the SCI service
    /// Note: At time of writing, SCI is calc using values from WEPP model for both WaterOM and WindOM, but values here are from the respective models
    /// </summary>
    public class ErosionParameters
    {
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public string RotationName { get; set; }

        public double WeppOM { get; set; }

        public double WepsOM { get; set; }

        public double Rusle2OM { get; set; }

        public double WeppFO { get; set; }

        public double WepsFO { get; set; }

        public double Rusle2FO { get; set; }

        public double WeppER { get; set; }

        public double WepsER { get; set; }

        public double Rusle2ER { get; set; }

        public double WeppStir { get; set; }

        public double WepsStir { get; set; }

        public double Rusle2Stir { get; set; }

        public double WepsWindErosion { get; set; }

        public double WeppSoilLoss { get; set; }

        public double Rusle2SoilLoss { get; set; }

        public double Rusle2Sci { get; set; }
    }
}
