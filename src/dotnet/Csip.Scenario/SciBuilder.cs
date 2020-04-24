﻿using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    /// <summary>
    /// Builds scenarios for CSIP SCI service
    /// </summary>
    public class SciBuilder
    {
        public string GetTemplate()
        {
            string template = "{\"metainfo\":{\"name\":\"SCI\",\"description\":\"SCI\",\"state\":\"Development\",\"service_url\":\"http://csip.engr.colostate.edu:8083/csip-sq/m/sci/2.1\"},\"parameter\":[{\"name\":\"erosion_water\",\"value\":0},{\"name\":\"water_om\",\"value\":0},{\"name\":\"water_fo\",\"value\":0},{\"name\":\"wind_om\",\"value\":0},{\"name\":\"wind_fo\",\"value\":0},{\"name\":\"erosion_wind\",\"value\":0}]}";

            return template;
        }

        public List<string> BuildScenarios(
            List<ErosionParameters> erosionParametersList,
            string templateJson)
        {
            List<string> scenarios = new List<string>();

            JObject jsonObj = JObject.Parse(templateJson);

            foreach(ErosionParameters erosionParameters in erosionParametersList)
            {
                string scenario =
                    AddParameters(jsonObj, erosionParameters)
                    .ToString();

                scenarios.Add(scenario);
            }
            
            return scenarios;
        }
        
        private JObject AddParameters(
            JObject scenario,
            ErosionParameters erosionParameters)
        {
            JObject withParameters = scenario;

            // Note: The components of the name are split using double underscore (__), not single, due to an underscore being used in RotationName
            string scenarioName =
                $"{erosionParameters.Latitude.ToString()}__{erosionParameters.Longitude.ToString()}__{erosionParameters.RotationName}";

            // Set name
            withParameters["metainfo"]["name"] = scenarioName;

            foreach(var p in withParameters["parameter"])
            {
                string paramName = p["name"].ToString();
                // Note: We're using the organic matter value from WEPP, so overriding wind_om with value from WEPP (wind_om = WaterOM)
                switch(paramName)
                {
                    case "erosion_water":
                        p["value"] = erosionParameters.WeppErosion;
                        break;
                    case "water_om":
                        p["value"] = erosionParameters.WeppOM;
                        break;
                    case "water_fo":
                        p["value"] = erosionParameters.WeppFO;
                        break;
                    case "wind_om":
                        p["value"] = erosionParameters.WeppOM;
                        break;
                    case "wind_fo":
                        p["value"] = erosionParameters.WepsFO;
                        break;
                    case "erosion_wind":
                        p["value"] = erosionParameters.WepsErosion;
                        break;
                }
            }

            return withParameters;
        }
    }
}
