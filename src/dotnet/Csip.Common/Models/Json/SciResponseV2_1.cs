using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    public class SciResponseV2_1
    {
        public string Suid { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string RotationName { get; set; }

        public double ErosionWater { get; set; }

        public double WaterOM { get; set; }

        public double WaterFO { get; set; }

        public double WindOM { get; set; }

        public double WindFO { get; set; }

        public double ErosionWind { get; set; }


        public double SciTotal { get; set; }
    }
}
