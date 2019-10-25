using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
