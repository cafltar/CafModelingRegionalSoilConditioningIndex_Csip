using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    public class WepsBuilder: IScenarioBuilder
    {
        // TODO: maybe use enum instead of path?
        public string GetTemplate()
        {
            string template = 
                File.ReadAllText(@"Assets\templates\templateWeps.json");              

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
                if (o["name"].ToString() == "soil")
                    o["value"] = cokey;
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
            return scenario;
        }

        public JObject AddSoilLength(
            JObject scenario,
            double soilLength)
        {
            JObject withSoilLength = scenario;

            foreach (var o in withSoilLength["parameter"])
            {
                if (o["name"].ToString() == "crlmod")
                {
                    o["value"]["rotationFiles"][0]["rotation"]["length"] = soilLength;
                }
            }

            return withSoilLength;
        }
    }
}
