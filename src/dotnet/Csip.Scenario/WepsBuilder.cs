using Newtonsoft.Json.Linq;
using System.IO;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario
{
    public class WepsBuilder: IBuildErosionModel
    {
        // TODO: maybe use enum instead of path?
        public string GetTemplate()
        {
            string template = "{\"metainfo\":{\"mode\":\"sync\"},\"parameter\":[{\"name\":\"crlmod\",\"value\":{\"rotationFiles\":[{\"rotation\":{\"duration\":1,\"length\":175,\"managements\":[{\"events\":[{\"date\":\"2020-05-15\",\"interval\":false,\"operation\":{\"id\":\"23173\",\"name\":\"Sprayer, post emergence\"},\"residue\":{\"id\":\"156\",\"name\":\"weed residue; 0-3 mo\",\"res_added\":100}},{\"date\":\"2020-06-15\",\"interval\":false,\"operation\":{\"id\":\"23169\",\"name\":\"Sprayer, post emerge, fungicide\"}},{\"date\":\"2020-07-01\",\"interval\":false,\"operation\":{\"id\":\"23169\",\"name\":\"Sprayer, post emerge, fungicide\"}},{\"date\":\"2020-08-15\",\"interval\":false,\"operation\":{\"id\":\"23269\",\"name\":\"Harvest, killing crop 50pct standing stubble\"}},{\"date\":\"2020-09-16\",\"interval\":false,\"operation\":{\"id\":\"23517\",\"name\":\"Plow, moldboard, 6 to 12 inch deep\"}},{\"date\":\"2020-09-17\",\"interval\":false,\"operation\":{\"id\":\"23422\",\"name\":\"Disk, tandem secondary\"}},{\"date\":\"2020-09-19\",\"interval\":false,\"operation\":{\"id\":\"23131\",\"name\":\"Fert applic. anhyd knife 12 inch spacing, coil tine har\"}},{\"crop\":{\"id\":\"972\",\"name\":\"Wheat, winter, grain\",\"yield\":85,\"yieldUnit\":\"bu/ac\"},\"date\":\"2020-09-20\",\"interval\":false,\"operation\":{\"id\":\"23331\",\"name\":\"Drill or air seeder, double disk\"}}],\"id\":null,\"name\":\"Wheat, winter, early plant, conv, fplow, Z47\",\"path\":\"Edited CMZ 47\\\\a.Single Year Single Crop Templates\",\"stir\":0}],\"name\":\"z47 Winter Wheat STIR-124\"}}]}},{\"name\":\"crop_calibration_mode\",\"value\":\"true\"},{\"name\":\"soil\",\"value\":\"15891285\"},{\"name\":\"usePRISM\",\"value\":true},{\"name\":\"field_shape\",\"value\":\"rectangle\"},{\"name\":\"field_length\",\"value\":4000},{\"name\":\"field_width\",\"value\":4000},{\"name\":\"field_radius\",\"value\":0},{\"name\":\"field_orientation\",\"value\":-0.15},{\"name\":\"latitude\",\"value\":\"47.053053617392\"},{\"name\":\"longitude\",\"value\":\"-117.240739873477\"},{\"name\":\"elevation\",\"value\":\"0\"},{\"name\":\"wind_barriers\",\"value\":[{\"name\":\"wind_barrier\",\"value\":null,\"properties\":{}},{\"name\":\"wind_barrier\",\"value\":null,\"properties\":{}},{\"name\":\"wind_barrier\",\"value\":null,\"properties\":{}},{\"name\":\"wind_barrier\",\"value\":null,\"properties\":{}}]},{\"name\":\"water_erosion_loss\",\"value\":0},{\"name\":\"soil_rock_fragments\",\"value\":0}]}";

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
