using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine
{
    public class SciJsonResponseToExperimentalResults
    {
        private readonly JsonHandler jsonHandler;
        private readonly SciV2_1 serviceHandler;
        private readonly CsvHandler csvHandler;

        public SciJsonResponseToExperimentalResults(
            JsonHandler jsonHandler,
            SciV2_1 serviceHandler,
            CsvHandler csvHandler)
        {
            this.jsonHandler = jsonHandler;
            this.serviceHandler = serviceHandler;
            this.csvHandler = csvHandler;
        }

        public bool Run(
            string inputCsipLocationsPath,
            string inputErosionParametersPath,
            string inputSciResponsePath,  
            string outputFilePath)
        {
            const int PRECISION = 5;
            // CsipLocations and ErosionParameters should be in csv format already, from previous steps
            List<CsipLocation> csipLocations = csvHandler.ReadCsipLocationFile(
                inputCsipLocationsPath);
            List<ErosionParameters> erosionParameters = csvHandler.ReadErosionParameters(
                inputErosionParametersPath);

            List<SciResponseV2_1> sciResponses =
                jsonHandler.ReadSciResponseV2_1Files(
                    inputSciResponsePath, serviceHandler);

            List<ExperimentalResults> experimentalResultsList =
                new List<ExperimentalResults>();

            foreach(SciResponseV2_1 sciResponse in sciResponses)
            {
                // Find matching scenarios
                CsipLocation csipLocation = csipLocations
                    .First(x => 
                            Math.Round(x.Latitude, PRECISION) == 
                                Math.Round(sciResponse.Latitude, PRECISION)
                        &&  Math.Round(x.Longitude, PRECISION) == 
                                Math.Round(sciResponse.Longitude, PRECISION));
                
                ErosionParameters erosionParameter = erosionParameters
                    .First(x => 
                            Math.Round(x.Latitude, PRECISION) == 
                                Math.Round(sciResponse.Latitude, PRECISION)
                        &&  Math.Round(x.Longitude, PRECISION) == 
                                Math.Round(sciResponse.Longitude, PRECISION)
                        &&  x.RotationName == sciResponse.RotationName);

                if (csipLocation == null || erosionParameter == null)
                    throw new Exception(
                        "Could not find matching CsipLocation and/or ErosionParameter for given SciResponse");

                ExperimentalResults result = new ExperimentalResults()
                {
                    // TODO: Create new column for actual WindOM and value used by SCI service?
                    Latitude = sciResponse.Latitude,
                    Longitude = sciResponse.Longitude,
                    RotationName = sciResponse.RotationName,
                    AnthromeKey = csipLocation.AnthromeKey,
                    Cokey = csipLocation.Cokey,
                    Slope = csipLocation.Slope,
                    SlopeLength = csipLocation.SlopeLength,
                    PercentOfMapUnit = csipLocation.PercentOfMapUnit,
                    MapUnitName = csipLocation.MapUnitName,
                    WaterOM = erosionParameter.WeppOM,
                    WindOM = erosionParameter.WepsOM,
                    WaterFO = erosionParameter.WeppFO,
                    WindFO = erosionParameter.WepsFO,
                    WaterErosion = erosionParameter.WeppER,
                    WindErosion = erosionParameter.WepsER,
                    SciTotal = sciResponse.SciTotal
                };

                experimentalResultsList.Add(result);
            }

            // Write file
            csvHandler.WriteExperimentalResultsParameters(
                outputFilePath,
                experimentalResultsList);

            return true;
        }
    }
}
