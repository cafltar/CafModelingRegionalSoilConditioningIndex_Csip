using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using System;
using System.Linq;
using System.Text.Json;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services
{
    public class WeppV3_1
    {
        public WeppResponseV3_1 ParseResultsJson(string jsonResult)
        {
            WeppResponseV3_1 metainfo = new WeppResponseV3_1();
            WeppResponseV3_1 parameters = new WeppResponseV3_1();
            WeppResponseV3_1 result = new WeppResponseV3_1();

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using (JsonDocument document = JsonDocument.Parse(jsonResult, options))
            {
                foreach (JsonProperty element in document.RootElement.EnumerateObject())
                {
                    string elementName = element.Name;
                    switch(elementName)
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

            WeppResponseV3_1 results = MergeResults(
                metainfo,
                parameters,
                result);

            return results;
        }

        private WeppResponseV3_1 ParseMetainfoElement(
            JsonProperty metainfoElement)
        {
            WeppResponseV3_1 results = new WeppResponseV3_1();

            foreach (var element in metainfoElement.Value.EnumerateObject())
            {
                string propName = element.Name;
                switch (propName)
                {
                    case "suid":
                        results.Suid = element.Value.GetString();
                        break;
                    case "status":
                        results.Status = element.Value.GetString();
                        break;
                }
            }

            return results;
        }

        private WeppResponseV3_1 ParseParameterElement(
            JsonProperty parameterElement)
        {
            WeppResponseV3_1 results = new WeppResponseV3_1();

            foreach (var element in parameterElement.Value.EnumerateArray())
            {
                string propName = element.GetProperty("name").GetString();
                switch (propName)
                {
                    case "latitude":
                        results.Latitude = Convert.ToDouble(
                            element.GetProperty("value").GetString());
                        break;
                    case "longitude":
                        results.Longitude = Convert.ToDouble(
                            element.GetProperty("value").GetString());
                        break;
                    case "crlmod":
                        results.RotationName = element.GetProperty("value")
                            .GetProperty("rotationFiles")
                            .EnumerateArray()
                            .First()
                            .EnumerateObject()
                            .First()
                            .Value
                            .EnumerateObject()
                            .First(x => x.Name == "name").Value.ToString();
                        break;
                }
            }

            return results;
        }

        private WeppResponseV3_1 ParseResultElement(
            JsonProperty resultElement)
        {
            WeppResponseV3_1 results = new WeppResponseV3_1();

            foreach (var element in resultElement.Value.EnumerateArray())
            {
                string propName = element.GetProperty("name").GetString();
                switch(propName)
                {
                    case "Precipitation":
                        results.Precipitation = element.GetProperty("value").GetDouble();
                        break;
                    case "SoilLoss":
                        results.SoilLoss = element.GetProperty("value").GetDouble();
                        break;
                    case "Runoff":
                        results.Runoff = element.GetProperty("value").GetDouble();
                        break;
                    case "SedimentYield":
                        results.SedimentYield = element.GetProperty("value").GetDouble();
                        break;
                    case "STIR":
                        results.Stir = element.GetProperty("value").GetDouble();
                        break;
                    case "OM":
                        results.OM = element.GetProperty("value").GetDouble();
                        break;
                    case "FO":
                        results.FO = element.GetProperty("value").GetDouble();
                        break;
                    case "ER":
                        results.ER = element.GetProperty("value").GetDouble();
                        break;
                }
            }

            return results;
        }

        private WeppResponseV3_1 MergeResults(
            WeppResponseV3_1 metainfo,
            WeppResponseV3_1 parameters,
            WeppResponseV3_1 result)
        {
            WeppResponseV3_1 results = new WeppResponseV3_1()
            {
                Suid = metainfo.Suid,
                Status = metainfo.Status,
                Latitude = parameters.Latitude,
                Longitude = parameters.Longitude,
                RotationName = parameters.RotationName,
                Precipitation = result.Precipitation,
                SoilLoss = result.SoilLoss,
                Runoff = result.Runoff,
                SedimentYield = result.SedimentYield,
                Stir = result.Stir,
                OM = result.OM,
                FO = result.FO,
                ER = result.ER
            };

            return results;
        }
    }
}
