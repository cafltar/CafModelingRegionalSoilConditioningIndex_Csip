using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services
{
    public class SciV2_1
    {
        public SciResponseV2_1 ParseResultsJson(string jsonResult)
        {
            SciResponseV2_1 response = new SciResponseV2_1();

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using (JsonDocument document = JsonDocument.Parse(jsonResult, options))
            {
                string name = document.RootElement.GetProperty("metainfo").GetProperty("name").GetString();

                string[] nameComponents = name.Split("__");
                response.Latitude = Convert.ToDouble(nameComponents[0]);
                response.Longitude = Convert.ToDouble(nameComponents[1]);
                response.RotationName = nameComponents[2];

                response.ErosionWater = document.RootElement.GetProperty("parameter")
                    .EnumerateArray()
                    .First(x => x.GetProperty("name").GetString() == "erosion_water")
                    .GetProperty("value").GetDouble();

                response.WaterOM = document.RootElement.GetProperty("parameter")
                    .EnumerateArray()
                    .First(x => x.GetProperty("name").GetString() == "water_om")
                    .GetProperty("value").GetDouble();

                response.WaterFO = document.RootElement.GetProperty("parameter")
                    .EnumerateArray()
                    .First(x => x.GetProperty("name").GetString() == "water_fo")
                    .GetProperty("value").GetDouble();

                response.WindOM = document.RootElement.GetProperty("parameter")
                    .EnumerateArray()
                    .First(x => x.GetProperty("name").GetString() == "wind_om")
                    .GetProperty("value").GetDouble();

                response.WindFO = document.RootElement.GetProperty("parameter")
                    .EnumerateArray()
                    .First(x => x.GetProperty("name").GetString() == "wind_fo")
                    .GetProperty("value").GetDouble();

                response.ErosionWind = document.RootElement.GetProperty("parameter")
                    .EnumerateArray()
                    .First(x => x.GetProperty("name").GetString() == "erosion_wind")
                    .GetProperty("value").GetDouble();

                response.SciTotal = document.RootElement.GetProperty("result")
                    .EnumerateArray()
                    .First(x => x.GetProperty("name").GetString() == "sci_total")
                    .GetProperty("value").GetDouble();
            }

            return response;
        }
    }
}
