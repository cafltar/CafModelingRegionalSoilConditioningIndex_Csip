using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    public class Rusle2ResponseV5_0 : IErosionModelResponse
    {
        public string Suid { get; set; }

        public string Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Name of rotation treatment
        public string RotationName { get; set; }

        // "name": "SEG_SOIL_LOSS",
        public double SoilLoss { get; set; }

        // #RD:SOIL_COND_INDEX_PTR:SOIL_COND_INDEX_STIR_VAL (unitless)
        public double Stir { get; set; }

        // #RD:SOIL_COND_INDEX_PTR:SOIL_COND_INDEX_OM_SUBFACTOR (unitless)
        public double OM { get; set; }

        // #RD:SOIL_COND_INDEX_PTR:SOIL_COND_INDEX_FO_SUBFACTOR (unitless)
        public double FO { get; set;  }

        // #RD:SOIL_COND_INDEX_PTR:SOIL_COND_INDEX_ER_SUBFACTOR (unitless)
        public double ER { get; set; }

        // "name": "#RD:SOIL_COND_INDEX_PTR:SOIL_COND_INDEX_RESULT" (unitless)
        public double SCI { get; set; }

    }
}
