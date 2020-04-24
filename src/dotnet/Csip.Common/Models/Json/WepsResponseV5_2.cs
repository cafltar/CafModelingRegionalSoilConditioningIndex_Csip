namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    /// <summary>
    /// Represents important values obtained from results of a WEPS simulation
    /// </summary>
    public class WepsResponseV5_2 : IErosionModelResponse
    {
        public string Suid { get; set; }

        public string Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Name of rotation treatment
        public string RotationName { get; set; }

        // wind_eros: Wind erosion (computed by WEPS) (ton/acre/year)
        public double WindErosion { get; set; }

        // sci_er_factor: SCI Erosion Rate subfactor (unitless)
        public double ER { get; set; }

        // sci_om_factor: SCI Organic Matter subfactor (unitless)
        public double OM { get; set; }

        // sci_fo_factor: SCI Field Operations subfactor (unitless)
        public double FO { get; set; }

        // avg_all_stir: Average STIR (unitless)
        public double Stir { get; set; }

    }
}
