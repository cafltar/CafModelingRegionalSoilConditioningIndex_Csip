using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Cokey
{
    public class CokeyChooser
    {
        // Returns a cokey string from 
        public string GetDominateCokey(WweSoilParamsV2Results mapUnits)
        {
            // TODO: Possibly return Mukey, percent, area, along with cokey

            // I love Linq
            string cokey = mapUnits.MapUnits
                .OrderByDescending(m => m.Area)
                .FirstOrDefault().Components
                    .OrderByDescending(c => c.Percent)
                    .FirstOrDefault().Cokey;

            return cokey;
        }
    }
}
