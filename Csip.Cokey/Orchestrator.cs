using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Cokey
{
    public class Orchestrator
    {
        private readonly CsvHandler fileHandler;
        private readonly WweSoilParamsV2_0 serviceHandler;
        private readonly PointToPolygonConverter converter;
        private readonly CokeyChooser cokeyChooser;

        public Orchestrator(
            CsvHandler fileHandler,
            WweSoilParamsV2_0 serviceHandler,
            PointToPolygonConverter converter,
            CokeyChooser cokeyChooser)
        {
            this.fileHandler = fileHandler;
            this.serviceHandler = serviceHandler;
            this.converter = converter;
            this.cokeyChooser = cokeyChooser;
        }

        public async Task<bool> Run(string inputFilePath, string outputFilePath)
        {
            List<Location> locations = new List<Location>();

            // Read file with FlexCropping locations
            List<FlexCroppingLocation> points = 
                fileHandler.ReadFlexCroppingLocationFile(inputFilePath);

            // For each point, get cokey
            foreach(var point in points)
            {
                string polygonString = converter.GetPixelAsBoundingBoxString(
                    point.Latitude, point.Longitude, 4);

                string resultJson = await serviceHandler.Post(polygonString);

                WweSoilParamsV2Results result = 
                    serviceHandler.ParseResultsJson(resultJson);

                string cokey = cokeyChooser.GetDominateCokey(result);

                Location location = new Location()
                {
                    Cokey = Convert.ToInt32(cokey),
                    Latitude = point.Latitude,
                    Longitude = point.Longitude
                };

                locations.Add(location);
            }

            // Write file
            fileHandler.WriteLocationFile(outputFilePath, locations);

            return true;
        }
    }
}
