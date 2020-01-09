using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Helpers;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine
{
    public class FlexCroppingLocationToCsipLocation
    {
        private readonly CsvHandler fileHandler;
        private readonly WweSoilParamsV2_0 serviceHandler;
        private readonly PointToPolygonConverter converter;
        private readonly CokeyChooser cokeyChooser;

        public FlexCroppingLocationToCsipLocation(
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
            List<CsipLocation> locations = new List<CsipLocation>();

            // Read file with FlexCropping locations
            List<FlexCroppingLocation> points = 
                fileHandler.ReadFlexCroppingLocationFile(inputFilePath);

            // For each point, get cokey
            foreach(var point in points)
            {
                string polygonString = converter.GetPixelAsBoundingBoxString(
                    point.Latitude, point.Longitude, 4);

                string resultJson = await serviceHandler.Post(polygonString);

                WweSoilParamsResponseV2_0 result = 
                    serviceHandler.ParseResultsJson(resultJson);

                //string cokey = cokeyChooser.GetDominateCokey(result);
                Component component = cokeyChooser.GetDominateComponent(result);
                string muname = cokeyChooser.GetDominateMapUnitName(result);

                CsipLocation location = new CsipLocation()
                {
                    Latitude = point.Latitude,
                    Longitude = point.Longitude,
                    AnthromeKey = point.Anthrome,
                    Cokey = component.Cokey,
                    Slope = component.Slope,
                    SlopeLength = component.SlopeLength,
                    PercentOfMapUnit = component.PercentOfMapUnit,
                    MapUnitName = muname
                };

                locations.Add(location);
            }

            // Write file
            fileHandler.WriteCsipLocationFile(outputFilePath, locations);

            return true;
        }
    }
}
