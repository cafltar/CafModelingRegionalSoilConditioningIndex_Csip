using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    public interface IScenarioBuilder
    {
        // TODO: maybe use enum instead of path?
        public string GetTemplate(string templateName)
        {

            var template = templateName switch
            {
                "wepp" => File.ReadAllText(@"Assets\templates\templateWepp.json"),
                "weps" => File.ReadAllText(@"Assets\templates\templateWeps.json"),
                _ => throw new ArgumentException("templateName not valid"),
            };
            return template;
        }

        public Dictionary<int, List<string>> GetRotations()
        {
            Dictionary<int, List<string>> rotations = new Dictionary<int, List<string>>
            {
                { 11, GetRotationJson(@"Assets\rot_20191106", "AnnualAEC*") },
                { 111, GetRotationJson(@"Assets\rot_20191106", "AnnualAEC*") },
                { 12, GetRotationJson(@"Assets\rot_20191106", "TransitionAEC*") },
                { 112, GetRotationJson(@"Assets\rot_20191106", "TransitionAEC*") },
                { 13, GetRotationJson(@"Assets\rot_20191106", "GrainFallowAEC*") },
                { 113, GetRotationJson(@"Assets\rot_20191106", "GrainFallowAEC*") }
            };

            return rotations;
        }

        private List<string> GetRotationJson(string rotationDir, string pattern)
        {
            List<string> rotationJsons = new List<string>();

            foreach (var f in Directory.GetFiles(rotationDir, pattern))
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

        public JObject AddLocation(
            JObject scenario,
            double latitude,
            double longitude);

        public JObject AddCokey(
            JObject scenario,
            int cokey);

        public JObject AddRotation(
            JObject scenario,
            string rotationJson);

        public string GetTemplate();
    }
}
