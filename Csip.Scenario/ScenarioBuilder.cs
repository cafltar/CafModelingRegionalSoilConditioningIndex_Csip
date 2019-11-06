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
    public class ScenarioBuilder
    {
        public Dictionary<int, List<string>> GetRotations()
        {
            Dictionary<int, List<string>> rotations = new Dictionary<int, List<string>>();

            rotations.Add(11, GetRotationJson(@"Assets\rot_20191106", "AnnualAEC*"));
            rotations.Add(111, GetRotationJson(@"Assets\rot_20191106", "AnnualAEC*"));
            rotations.Add(12, GetRotationJson(@"Assets\rot_20191106", "TransitionAEC*"));
            rotations.Add(112, GetRotationJson(@"Assets\rot_20191106", "TransitionAEC*"));
            rotations.Add(13, GetRotationJson(@"Assets\rot_20191106", "GrainFallowAEC*"));
            rotations.Add(113, GetRotationJson(@"Assets\rot_20191106", "GrainFallowAEC*"));

            return rotations;
        }

        private List<string> GetRotationJson(string rotationDir, string pattern)
        {
            List<string> rotationJsons = new List<string>();

            foreach(var f in Directory.GetFiles(rotationDir, pattern))
            {
                rotationJsons.Add(File.ReadAllText(f));
            }

            return rotationJsons;
        }
        
        public List<string> BuildScenarios(
            List<Location> locations,
            string templateJson,
            Dictionary<int, List<string>> rotations)
        {
            List<string> scenarios = new List<string>();

            JObject jsonObj = JObject.Parse(templateJson);

            foreach (Location location in locations)
            {
                foreach (string rotation in rotations[location.AnthromeKey])
                {

                    // TODO: Ugly! Implement fluent pattern here
                    string scenario =
                        AddRotation(
                            AddCokey(
                                AddLocation(
                                    jsonObj,
                                    location.Latitude,
                                    location.Longitude),
                                location.Cokey),
                            rotation)
                        .ToString();

                    scenarios.Add(scenario);
                }
            }
            
            return scenarios;
        }

        private JObject AddLocation(
            dynamic scenario,
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

        private JObject AddCokey(
            JObject scenario, 
            int cokey)
        {
            JObject withCokey = scenario;

            foreach(var o in withCokey["parameter"])
            {
                if (o["name"].ToString() == "soilPtr")
                    o["value"][0] = cokey.ToString();
            }

            return withCokey;
        }

        private JObject AddRotation(
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
    }
}
