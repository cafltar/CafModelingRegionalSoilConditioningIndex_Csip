using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    /// <summary>
    /// Builds scenarios for CSIP SCI service
    /// </summary>
    public interface IBuildSciModel
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

        public JObject AddParameters(
            JObject scenario,
            ErosionParameters erosionParameters);
    }
}
