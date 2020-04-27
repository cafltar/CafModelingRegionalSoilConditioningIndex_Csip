using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    public class SciBuilderRusle2: IBuildSciModel
    {
        public JObject AddParameters(
            JObject scenario,
            ErosionParameters erosionParameters)
        {
            JObject withParameters = scenario;

            // Note: The components of the name are split using double underscore (__), not single, due to an underscore being used in RotationName
            string scenarioName =
                $"{erosionParameters.Latitude.ToString()}__{erosionParameters.Longitude.ToString()}__{erosionParameters.RotationName}";

            // Set name
            withParameters["metainfo"]["name"] = scenarioName;

            foreach (var p in withParameters["parameter"])
            {
                string paramName = p["name"].ToString();
                // Note: We're using the organic matter value from WEPP, so overriding wind_om with value from WEPP (wind_om = WaterOM)
                switch (paramName)
                {
                    case "erosion_water":
                        p["value"] = erosionParameters.Rusle2ER;
                        break;
                    case "water_om":
                        p["value"] = erosionParameters.Rusle2OM;
                        break;
                    case "water_fo":
                        p["value"] = erosionParameters.Rusle2FO;
                        break;
                    case "wind_om":
                        p["value"] = erosionParameters.Rusle2OM;
                        break;
                    case "wind_fo":
                        p["value"] = erosionParameters.WepsFO;
                        break;
                    case "erosion_wind":
                        p["value"] = erosionParameters.WepsER;
                        break;
                }
            }

            return withParameters;
        }
    }
}
