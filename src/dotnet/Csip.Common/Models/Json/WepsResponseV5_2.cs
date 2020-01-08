namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    /// <summary>
    /// Represents important values obtained from results of a WEPS simulation
    /// </summary>
    public class WepsResponseV5_2
    {
        public string Suid { get; set; }

        public string Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Name of rotation treatment
        public string RotationName { get; set; }

        // Wind erosion (computed by WEPS) (ton/acre/year)
        public double WindErosion { get; set; }

        // SCI Erosion Rate subfactor (unitless)
        public double SciErosionFactor { get; set; }
    }
}
