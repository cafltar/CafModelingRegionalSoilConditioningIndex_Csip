using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services
{
    public class WepsV5_2
    {
        public WepsResponseV5_2 ParseResultsJson(string jsonResult)
        {
            WepsResponseV5_2 metainfo = new WepsResponseV5_2();
            WepsResponseV5_2 parameters = new WepsResponseV5_2();
            WepsResponseV5_2 result = new WepsResponseV5_2();

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using (JsonDocument document = JsonDocument.Parse(jsonResult, options))
            {
                foreach (JsonProperty element in document.RootElement.EnumerateObject())
                {
                    string elementName = element.Name;
                    switch (elementName)
                    {
                        case "metainfo":
                            metainfo = ParseMetainfoElement(element);
                            break;
                        case "parameter":
                            parameters = ParseParameterElement(element);
                            break;
                        case "result":
                            result = ParseResultElement(element);
                            break;
                    }
                }
            }

            WepsResponseV5_2 results = MergeResults(
                metainfo,
                parameters,
                result);

            return results;
        }

        private WepsResponseV5_2 ParseMetainfoElement(
            JsonProperty metainfoElement)
        {
            WepsResponseV5_2 result = new WepsResponseV5_2();

            foreach (var element in metainfoElement.Value.EnumerateObject())
            {
                string propName = element.Name;
                switch(propName)
                {
                    case "suid":
                        result.Suid = element.Value.GetString();
                        break;
                    case "status":
                        result.Status = element.Value.GetString();
                        break;
                }
            }

            return result;
        }

        private WepsResponseV5_2 ParseParameterElement(
            JsonProperty parameterElement)
        {
            WepsResponseV5_2 result = new WepsResponseV5_2();

            foreach (var element in parameterElement.Value.EnumerateArray())
            {
                string propName = element.GetProperty("name").GetString();
                switch(propName)
                {
                    case "latitude":
                        result.Latitude = Convert.ToDouble(
                            element.GetProperty("value").GetString());
                        break;
                    case "longitude":
                        result.Longitude = Convert.ToDouble(
                            element.GetProperty("value").GetString());
                        break;
                    case "crlmod":
                        result.RotationName = element
                            .GetProperty("value")
                            .GetProperty("rotationFiles")
                            .EnumerateArray().First()
                            .GetProperty("rotation")
                            .GetProperty("name").GetString();
                        break;
                }
            }

            return result;
        }

        private WepsResponseV5_2 ParseResultElement(
            JsonProperty resultElement)
        {
            WepsResponseV5_2 result = new WepsResponseV5_2();

            foreach (var element in resultElement.Value.EnumerateArray())
            {
                string propName = element.GetProperty("name").GetString();
                switch(propName)
                {
                    case "wind_eros":
                        result.WindErosion = element.GetProperty("value").GetDouble();
                        break;
                    case "sci_er_factor":
                        result.ER = element.GetProperty("value").GetDouble();
                        break;
                    case "sci_om_factor":
                        result.OM = element.GetProperty("value").GetDouble();
                        break;
                    case "sci_fo_factor":
                        result.FO = element.GetProperty("value").GetDouble();
                        break;
                    case "avg_all_stir":
                        result.Stir = element.GetProperty("value").GetDouble();
                        break;
                }
            }

            return result;
        }

        private WepsResponseV5_2 MergeResults(
            WepsResponseV5_2 metainfo,
            WepsResponseV5_2 parameters,
            WepsResponseV5_2 result)
        {
            WepsResponseV5_2 results = new WepsResponseV5_2()
            {
                Suid = metainfo.Suid,
                Status = metainfo.Status,
                Latitude = parameters.Latitude,
                Longitude = parameters.Longitude,
                RotationName = parameters.RotationName,
                WindErosion = result.WindErosion,
                ER = result.ER,
                FO = result.FO,
                OM = result.OM,
                Stir = result.Stir
            };

            return results;
        }
    }
}
