using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    public class Rusle2ResponseV5_0
    {
        public string Suid { get; set; }

        public string Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Name of rotation treatment
        public string RotationName { get; set; }

        // "name": "#RD:SOIL_COND_INDEX_PTR:SOIL_COND_INDEX_RESULT" (unitless)
        public double SCI { get; set; }

        //  "name": "SLOPE_DELIVERY"
        public double SlopeDelivery { get; set; }

        // "name": "SLOPE_DEGRAD"
        public double SlopeDegrad { get; set; }

        // "name": "SEG_SOIL_LOSS",
        public double SegSoilLoss { get; set; }

    }
}
