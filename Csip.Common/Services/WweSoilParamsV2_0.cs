using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services
{
    
    public class WweSoilParamsV2_0
    {
        private readonly HttpClient client;
        private readonly string endpointPath;

        public WweSoilParamsV2_0(HttpClient client)
        {
            this.client = client;
            this.client.BaseAddress = new Uri(
                "http://csip.engr.colostate.edu:8083/");
            this.endpointPath = "csip-soils/d/wwesoilparams/2.0";
        }

        public async Task<string> Post(string jsonPolygon)
        {
            string jsonContent = BuildJsonContent(jsonPolygon);
            HttpContent httpContent = new StringContent(
                jsonContent, Encoding.UTF8, "application/json");

            // TODO: Add better error handling
            var response = await client.PostAsync(endpointPath, httpContent);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                throw new Exception(
                    $"Http request failed: {response.StatusCode.ToString()}");
            }  
        }

        public WweSoilParamsV2Results ParseResultsJson(string jsonResult)
        {
            // TODO: Read error message, include in returned object

            WweSoilParamsV2Results result = new WweSoilParamsV2Results();

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using (JsonDocument document = JsonDocument.Parse(jsonResult, options))
            {
                foreach (JsonProperty element in document.RootElement.EnumerateObject())
                {
                    if(element.Name == "result")
                    {
                        result = ParseResultElement(element);
                    }
                }
            }

            return result;
        }

        private WweSoilParamsV2Results ParseResultElement(JsonProperty resultElement)
        {
            WweSoilParamsV2Results results = new WweSoilParamsV2Results();

            foreach(var element in resultElement.Value.EnumerateArray())
            {
                if(element.GetProperty("name").GetString() == "AoA Area")
                    results.MapUnitMeta = JsonSerializer
                        .Deserialize<MapUnitMeta>(element.ToString());

                if (element.GetProperty("name").GetString() == "Map Units")
                    results.MapUnits = ParseMapUnits(element);
            }

            return results;
        }

        private List<MapUnit> ParseMapUnits(JsonElement mapUnitsElement)
        {
            List<MapUnit> mapUnits = new List<MapUnit>();

            // This object have "value" = [[{},{},...,{}]], so need to drill down
            JsonElement mapUnitArray = mapUnitsElement.GetProperty("value");
            
            foreach(var mapUnitElement in mapUnitArray.EnumerateArray())
            {
                MapUnit mapUnit = new MapUnit();

                foreach(var element in mapUnitElement.EnumerateArray())
                {
                    if (element.GetProperty("name").GetString() == "mukey")
                        mapUnit.Mukey = element.GetProperty("value").GetString();

                    if (element.GetProperty("name").GetString() == "muname")
                        mapUnit.Name = element.GetProperty("value").GetString();

                    if (element.GetProperty("name").GetString() == "area")
                        mapUnit.Area = Convert.ToDouble(
                            element.GetProperty("value").GetString());

                    if (element.GetProperty("name").GetString() == "Components")
                        mapUnit.Components = ParseComponents(element);
                }

                mapUnits.Add(mapUnit);
            }
  
            return mapUnits;
        }

        private List<Component> ParseComponents(JsonElement componentsElement)
        {
            List<Component> components = new List<Component>();

            // This object have "value" = [[{},{},...,{}]], so need to drill down
            JsonElement componentArray = componentsElement.GetProperty("value");

            foreach(var componentElement in componentArray.EnumerateArray())
            {
                Component component = new Component();

                foreach (var element in componentElement.EnumerateArray())
                {
                    if (element.GetProperty("name").GetString() == "cokey")
                        component.Cokey = element.GetProperty("value").GetString();

                    if (element.GetProperty("name").GetString() == "compname")
                        component.Name = element.GetProperty("value").GetString();

                    if (element.GetProperty("name").GetString() == "comppct_r")
                        component.Percent = Convert.ToDouble(
                            element.GetProperty("value").GetString());
                }

                components.Add(component);
            }

            return components;
        }

        public string BuildJsonContent(string jsonPolygon)
        {
            // TODO: Probably should create models to serialize
            //string jsonContent = $"\{\"metainfo\":\{\}";
            string jsonContent = new StringBuilder()
                .Append(@"{""metainfo"":{},""parameter"":[{""name"":""aoa_geometry"",""type"":""Polygon"",""coordinates"":[")
                .Append(jsonPolygon)
                .Append(@"]}]}")
                .ToString();

            return jsonContent;
        }
    }
}
