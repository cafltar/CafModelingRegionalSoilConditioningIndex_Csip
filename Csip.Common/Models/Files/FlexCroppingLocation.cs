using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models
{
    public class FlexCroppingLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        
        [Name("MUKEY")]
        public int Mukey { get; set; }
        
        [Name("anthrome_4")]
        public int Anthrome { get; set; }
        
        [Name("elevat_mas")]
        public double Elevation { get; set; }
    }
}
