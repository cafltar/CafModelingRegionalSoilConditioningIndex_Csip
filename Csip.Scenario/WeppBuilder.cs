using Newtonsoft.Json.Linq;
using System.IO;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    public class WeppBuilder: IBuildErosionModel
    {
        // TODO: maybe use enum instead of path?
        public string GetTemplate()
        {
            string template = 
                File.ReadAllText(@"Assets\templates\templateWepp.json");              

            return template;
        }

        public JObject AddLocation(
            JObject scenario,
            double latitude,
            double longitude)
        {
            JObject withLocation = scenario;

            foreach (var o in withLocation["parameter"])
            {
                if (o["name"].ToString() == "latitude")
                    o["value"] = latitude.ToString();

                if (o["name"].ToString() == "longitude")
                    o["value"] = longitude.ToString();
            }
            
            return withLocation;
        }

        public JObject AddCokey(
            JObject scenario, 
            string cokey)
        {
            JObject withCokey = scenario;

            foreach(var o in withCokey["parameter"])
            {
                if (o["name"].ToString() == "soilPtr")
                    o["value"][0] = cokey;
            }

            return withCokey;
        }

        public JObject AddRotation(
            JObject scenario, 
            string rotationJson)
        {
            JObject withRotation = scenario;
            JObject rotation = JObject.Parse(rotationJson);

            foreach(var o in withRotation["parameter"])
            {
                if(o["name"].ToString() == "crlmod")
                {
                    o["value"]["rotationFiles"][0]["rotation"] = rotation;
                }
            }

            return withRotation;
        }

        public JObject AddSoilSlope(
            JObject scenario,
            double soilSlope)
        {
            JObject withSoilSlope = scenario;
            
            foreach(var o in withSoilSlope["parameter"])
            {
                if (o["name"].ToString() == "slope_steepness")
                {
                    o["value"] = soilSlope;
                }
            }

            return withSoilSlope;
        }

        public JObject AddSoilLength(
            JObject scenario,
            double soilLength)
        {
            JObject withSoilLength = scenario;

            foreach(var o in withSoilLength["parameter"])
            {
                if(o["name"].ToString() == "length")
                {
                    o["value"] = soilLength;
                }

                if(o["name"].ToString() == "crlmod")
                {
                    o["value"]["rotationFiles"][0]["rotation"]["length"] = soilLength;
                }
            }

            return withSoilLength;
        }
    }
}
