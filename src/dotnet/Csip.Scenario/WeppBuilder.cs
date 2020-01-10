using Newtonsoft.Json.Linq;
using System.IO;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    public class WeppBuilder: IBuildErosionModel
    {
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

        // Copied template from assets folder, minified and escaped, pasted here
        public string GetTemplate()
        {
            string template =
                "{\"metainfo\":{\"request-results\":[\"SLOPE_DELIVERY\",\"SLOPE_T_VALUE\",\"SLOPE_DEGRAD\"],\"mode\":\"sync\"},\"parameter\":[{\"name\":\"climateDataVersion\",\"value\":\"2015\"},{\"name\":\"usePRISM\",\"value\":true},{\"name\":\"latitude\",\"value\":47.053055},{\"name\":\"longitude\",\"value\":-117.24074},{\"name\":\"state\",\"value\":\"WA\"},{\"name\":\"stationName\",\"value\":\"Whitman County, Washington\"},{\"name\":\"aspect\",\"value\":180},{\"name\":\"width\",\"value\":50},{\"name\":\"slope_steepness\",\"value\":16},{\"name\":\"length\",\"value\":175},{\"name\":\"slope_type\",\"value\":\"Uniform\"},{\"name\":\"soilPtr\",\"value\":[\"17517364\"]},{\"name\":\"contour\",\"value\":\"(none)\"},{\"name\":\"crlmod\",\"value\":{\"rotationFiles\":[{\"rotation\":{\"duration\":1,\"length\":175,\"managements\":[{\"events\":[{\"date\":\"2020-05-15\",\"interval\":false,\"operation\":{\"id\":\"23173\",\"name\":\"Sprayer, post emergence\"},\"residue\":{\"id\":\"156\",\"name\":\"weed residue; 0-3 mo\",\"res_added\":100}},{\"date\":\"2020-06-15\",\"interval\":false,\"operation\":{\"id\":\"23169\",\"name\":\"Sprayer, post emerge, fungicide\"}},{\"date\":\"2020-07-01\",\"interval\":false,\"operation\":{\"id\":\"23169\",\"name\":\"Sprayer, post emerge, fungicide\"}},{\"date\":\"2020-08-15\",\"interval\":false,\"operation\":{\"id\":\"23269\",\"name\":\"Harvest, killing crop 50pct standing stubble\"}},{\"date\":\"2020-09-16\",\"interval\":false,\"operation\":{\"add_residue\":false,\"begin_growth\":false,\"defaultResidueAdded\":0,\"id\":\"23517\",\"kill_crop\":true,\"name\":\"Plow, moldboard, 6 to 12 inch deep\"}},{\"date\":\"2020-09-17\",\"interval\":false,\"operation\":{\"id\":\"23422\",\"name\":\"Disk, tandem secondary\"}},{\"date\":\"2020-09-19\",\"interval\":false,\"operation\":{\"id\":\"23131\",\"name\":\"Fert applic. anhyd knife 12 inch spacing, coil tine har\"}},{\"crop\":{\"id\":\"972\",\"name\":\"Wheat, winter, grain\",\"yield\":85,\"yieldUnit\":\"bu/ac\"},\"date\":\"2020-09-20\",\"interval\":false,\"operation\":{\"id\":\"23331\",\"name\":\"Drill or air seeder, double disk\"}}],\"id\":null,\"name\":\"Wheat, winter, early plant, conv, fplow, Z47\",\"path\":\"Edited CMZ 47\\\\a.Single Year Single Crop Templates\",\"stir\":0}],\"name\":\"z47 Winter Wheat STIR-124\"}}]}}]}";

            return template;
        }
    }
}
