using CsvHelper.Configuration.Attributes;
using System;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models
{
    public class CsipLocation
    {
        [Index(0)]
        public double Latitude { get; set; }
        
        [Index(1)]
        public double Longitude { get; set; }

        [Index(2)]
        public int AnthromeKey { get; set; }

        [Index(3)]
        public string Cokey { get; set; }

        [Index(4)]
        public double Slope { get; set; }

        [Index(5)]
        public double SlopeLength { get; set; }

        [Index(6)]
        public double PercentOfMapUnit { get; set; }

        [Index(7)]
        public string MapUnitName { get; set; }
    }
}
