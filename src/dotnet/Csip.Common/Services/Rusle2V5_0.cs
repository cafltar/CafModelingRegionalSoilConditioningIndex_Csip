using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services
{
    public class Rusle2V5_0
    {
        public Rusle2ResponseV5_0 ParseResultsJson(string jsonResults)
        {
            Rusle2ResponseV5_0 metainfo = new Rusle2ResponseV5_0();
            Rusle2ResponseV5_0 parameters = new Rusle2ResponseV5_0();
            Rusle2ResponseV5_0 result = new Rusle2ResponseV5_0();

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using (JsonDocument document = JsonDocument.Parse(jsonResults, options))
            {
                foreach(JsonProperty element in document.RootElement.EnumerateObject())
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


            Rusle2ResponseV5_0 results = MergeResults(
                metainfo,
                parameters,
                result);

            return results;
        }

        private Rusle2ResponseV5_0 ParseMetainfoElement(
            JsonProperty metainfoElement)
        {
            Rusle2ResponseV5_0 result = new Rusle2ResponseV5_0();

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

        private Rusle2ResponseV5_0 ParseParameterElement(
            JsonProperty parameterElement)
        {
            Rusle2ResponseV5_0 result = new Rusle2ResponseV5_0();

            foreach (var element in parameterElement.Value.EnumerateArray())
            {
                string propName = element.GetProperty("name").GetString();
                switch (propName)
                {
                    case "latitude":
                        result.Latitude = Convert.ToDouble(
                            element.GetProperty("value").GetString());
                        break;
                    case "longitude":
                        result.Longitude = Convert.ToDouble(
                            element.GetProperty("value").GetString());
                        break;
                    case "managements":
                        result.RotationName = element
                            .GetProperty("value")
                            .EnumerateArray().First()
                            .GetProperty("lmod_file")
                            .GetProperty("name")
                            .GetString();
                        break;
                }
            }

            return result;
        }

        private Rusle2ResponseV5_0 ParseResultElement(
            JsonProperty resultElement)
        {
            Rusle2ResponseV5_0 result = new Rusle2ResponseV5_0();

            foreach (var element in resultElement.Value.EnumerateArray())
            {
                string propName = element.GetProperty("name").GetString();
                switch (propName)
                {
                    case "#RD:SOIL_COND_INDEX_PTR:SOIL_COND_INDEX_RESULT":
                        result.SCI =
                            Convert.ToDouble(
                                element.GetProperty("value").GetString());
                        break;
                    case "SLOPE_DELIVERY":
                        result.SlopeDelivery =
                            Convert.ToDouble(
                                element.GetProperty("value").GetString());
                        break;
                    case "SLOPE_DEGRAD":
                        result.SlopeDegrad =
                            Convert.ToDouble(
                                element.GetProperty("value").GetString());
                        break;
                    case "SEG_SOIL_LOSS":
                        
                        try
                        {
                            result.SegSoilLoss =
                                element.GetProperty("value")[0].GetDouble();
                        }
                        catch(Exception e)
                        {
                            result.SegSoilLoss = -9999.9;
                        }
                        
                        
                        break;
                }
            }

            return result;
        }

        private Rusle2ResponseV5_0 MergeResults(
            Rusle2ResponseV5_0 metainfo,
            Rusle2ResponseV5_0 parameters,
            Rusle2ResponseV5_0 result)
        {
            Rusle2ResponseV5_0 results = new Rusle2ResponseV5_0()
            {
                Suid = metainfo.Suid,
                Status = metainfo.Status,
                Latitude = parameters.Latitude,
                Longitude = parameters.Longitude,
                RotationName = parameters.RotationName,
                SCI = result.SCI,
                SlopeDelivery = result.SlopeDelivery,
                SlopeDegrad = result.SlopeDegrad,
                SegSoilLoss = result.SegSoilLoss
            };

            return results;
        }
    }
}
