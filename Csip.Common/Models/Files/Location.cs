using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models
{
    public class Location
    {
        [Index(0)]
        public double Latitude { get; set; }
        
        [Index(1)]
        public double Longitude { get; set; }
        
        [Index(2)]
        public int Cokey { get; set; }

        [Index(3)]
        public int AnthromeKey { get; set; }
    }
}
