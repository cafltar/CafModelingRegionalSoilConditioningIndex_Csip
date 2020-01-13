using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine
{
    public class WeppWepsJsonResponseToErosionParameters
    {
        private readonly JsonHandler jsonHandler;
        private readonly WeppV3_1 weppV3_1Service;
        private readonly WepsV5_2 wepsV5_2Service;
        private readonly CsvHandler csvHandler;

        public WeppWepsJsonResponseToErosionParameters(
            JsonHandler jsonHandler,
            WeppV3_1 weppV3_1Service,
            WepsV5_2 wepsV5_2Service,
            CsvHandler csvHandler)
        {
            this.jsonHandler = jsonHandler;
            this.weppV3_1Service = weppV3_1Service;
            this.wepsV5_2Service = wepsV5_2Service;
            this.csvHandler = csvHandler;
        }
        public bool Run(
            string inputWeppPath, 
            string inputWepsPath, 
            string outputFilePath)
        {
            // Read json files with simulation results
            List<WeppResponseV3_1> weppResponses = 
                jsonHandler.ReadWeppResponseV3_1Files(
                    inputWeppPath, weppV3_1Service);
            List<WepsResponseV5_2> wepsResponses =
                jsonHandler.ReadWepsResponseV5_2Files(
                    inputWepsPath, wepsV5_2Service);

            if (weppResponses.Count != wepsResponses.Count)
                throw new ArgumentException("Input paths do not contain the same number of files");

            // Merge simulation results into erosion parameters
            List<ErosionParameters> erosionParametersList = 
                new List<ErosionParameters>();

            foreach(WeppResponseV3_1 weppResponse in weppResponses)
            {
                WepsResponseV5_2 wepsResponse =
                    wepsResponses.First(
                        x => x.Latitude == weppResponse.Latitude 
                          && x.Longitude == weppResponse.Longitude 
                          && x.RotationName == weppResponse.RotationName);

                if(wepsResponse != null)
                {
                    ErosionParameters erosionParameters = GetErosionParameters(
                        weppResponse,
                        wepsResponse);

                    erosionParametersList.Add(erosionParameters);
                }
                else { 
                    throw new Exception(
                        "Could not find matching weps response for given wepp response"); 
                }
            }

            // Write file
            csvHandler.WriteErosionParameters(
                outputFilePath, 
                erosionParametersList);
            
            return true;
        }

        private ErosionParameters GetErosionParameters(
            WeppResponseV3_1 weppResponse,
            WepsResponseV5_2 wepsResponse)
        {
            ErosionParameters result = new ErosionParameters()
            {
                Latitude = weppResponse.Latitude,
                Longitude = weppResponse.Longitude,
                RotationName = weppResponse.RotationName,
                WaterOM = weppResponse.OM,
                WindOM = wepsResponse.OM,
                WaterFO = weppResponse.FO,
                WindFO = wepsResponse.FO,
                WaterErosion = weppResponse.ER,
                WindErosion = wepsResponse.ER
            };

            return result;
        }
    }
}
