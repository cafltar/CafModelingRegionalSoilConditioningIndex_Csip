namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    /// <summary>
    /// Represents important values obtained from results of a WEPP simulation
    /// </summary>
    public class WeppResponseV3_1
    {
        public string Suid { get; set; }

        public string Status { get; set; }

        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        // Name of rotation treatment
        public string RotationName { get; set; }

        // Average Annual Preciptation (in)
        public double Precipitation { get; set; }

        // Average Annual Soil Loss (ton/ac/yr)
        public double SoilLoss { get; set; }

        // Average Annual Runoff
        public double Runoff { get; set; }

        // Average Annual Sediment Yield
        public double SedimentYield { get; set; }

        // STIR (unitless?)
        public double Stir { get; set; }

        // OM Factor of SCI (unitless)
        public double OM { get; set; }

        // FO Factor of SCI (unitless)
        public double FO { get; set; }

        // ER Factor of SCI (unitless)
        public double ER { get; set; }
    }
}
