using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    public interface IErosionModelResponse
    {
        public string Suid { get; set; }

        public string Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Name of rotation treatment
        public string RotationName { get; set; }
    }
}
