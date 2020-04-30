namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files
{
    /// <summary>
    /// A combination of all variables used to calculate SciTotal
    /// </summary>
    public class ExperimentalResults
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string RotationName { get; set; }

        public int AnthromeKey { get; set; }

        public string Cokey { get; set; }

        public double Slope { get; set; }

        public double SlopeLength { get; set; }

        public double PercentOfMapUnit { get; set; }

        public string MapUnitName { get; set; }

        public double WaterOM { get; set; }

        public double WindOM { get; set; }

        public double WaterFO { get; set; }

        public double WindFO { get; set; }

        public double ErosionWater { get; set; }

        public double ErosionWind { get; set; }

        public double SciTotal { get; set; }
    }
}
