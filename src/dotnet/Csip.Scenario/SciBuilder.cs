using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
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
            string template =
                File.ReadAllText(@"Assets\templates\templateSci.json");

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
                switch(paramName)
                {
                    case "erosion_water":
                        p["value"] = erosionParameters.WaterErosion;
                        break;
                    case "water_om":
                        p["value"] = erosionParameters.WaterOM;
                        break;
                    case "water_fo":
                        p["value"] = erosionParameters.WaterFO;
                        break;
                    case "wind_om":
                        p["value"] = erosionParameters.WindOM;
                        break;
                    case "wind_fo":
                        p["value"] = erosionParameters.WindFO;
                        break;
                    case "erosion_wind":
                        p["value"] = erosionParameters.WindErosion;
                        break;
                }
            }

            return withParameters;
        }
    }
}
