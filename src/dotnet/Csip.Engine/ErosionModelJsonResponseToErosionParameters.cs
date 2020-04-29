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
    public class ErosionModelJsonResponseToErosionParameters
    {
        private readonly JsonHandler jsonHandler;
        private readonly WeppV3_1 weppV3_1Service;
        private readonly WepsV5_2 wepsV5_2Service;
        private readonly Rusle2V5_0 rusle2V5_0Service;
        private readonly CsvHandler csvHandler;

        public ErosionModelJsonResponseToErosionParameters(
            JsonHandler jsonHandler,
            WeppV3_1 weppV3_1Service,
            WepsV5_2 wepsV5_2Service,
            Rusle2V5_0 rusle2V5_0Service,
            CsvHandler csvHandler)
        {
            this.jsonHandler = jsonHandler;
            this.weppV3_1Service = weppV3_1Service;
            this.wepsV5_2Service = wepsV5_2Service;
            this.rusle2V5_0Service = rusle2V5_0Service;
            this.csvHandler = csvHandler;
        }
        public bool Run(
            string inputWeppPath, 
            string inputWepsPath, 
            string inputRusle2Path,
            string outputFilePath)
        {
            // Read json files with simulation results
            List<WeppResponseV3_1> weppResponses = 
                jsonHandler.ReadWeppResponseV3_1Files(
                    inputWeppPath, weppV3_1Service);
            List<WepsResponseV5_2> wepsResponses =
                jsonHandler.ReadWepsResponseV5_2Files(
                    inputWepsPath, wepsV5_2Service);
            List<Rusle2ResponseV5_0> rusle2Responses =
                jsonHandler.ReadRusle2ResponseV5_0Files(
                    inputRusle2Path, rusle2V5_0Service);

            /* I should allow for failed runs - SciBuilder should check for missing values and skip those
            if ((weppResponses.Count != wepsResponses.Count) ||
                (weppResponses.Count != rusle2Responses.Count))
                throw new ArgumentException("Input paths do not contain the same number of files");
            */

            // Merge simulation results into erosion parameters
            // Using WEPS results to merge because when calc "SCI" both "old" and "new" versions require WEPS
            List<ErosionParameters> erosionParametersList = 
                new List<ErosionParameters>();

            foreach(WepsResponseV5_2 wepsResponse in wepsResponses)
            {
                WeppResponseV3_1 weppResponse =
                    weppResponses.First(
                        x => x.Latitude == wepsResponse.Latitude 
                          && x.Longitude == wepsResponse.Longitude 
                          && x.RotationName == wepsResponse.RotationName);
                Rusle2ResponseV5_0 rusle2Response =
                    rusle2Responses.First(
                        x => x.Latitude == wepsResponse.Latitude
                          && x.Longitude == wepsResponse.Longitude
                          && x.RotationName == wepsResponse.RotationName);

                ErosionParameters erosionParameters = GetErosionParameters(
                    wepsResponse,
                    weppResponse,
                    rusle2Response);

                    erosionParametersList.Add(erosionParameters);
            }

            // Write file
            csvHandler.WriteErosionParameters(
                outputFilePath, 
                erosionParametersList);
            
            return true;
        }

        private ErosionParameters GetErosionParameters(
            WepsResponseV5_2 wepsResponse,
            WeppResponseV3_1 weppResponse,
            Rusle2ResponseV5_0 rusle2Response)
        {
            double null_flag = -9999.99;

            ErosionParameters result = new ErosionParameters()
            {
                Latitude = wepsResponse.Latitude,
                Longitude = wepsResponse.Longitude,
                RotationName = wepsResponse.RotationName,
                WepsOM = wepsResponse.OM,
                WeppOM = weppResponse?.OM ?? null_flag,
                Rusle2OM = rusle2Response?.OM ?? null_flag,
                WepsFO = wepsResponse.FO,
                WeppFO = weppResponse?.FO ?? null_flag,
                Rusle2FO = rusle2Response?.FO ?? null_flag,
                WepsER = wepsResponse.ER,
                WeppER = weppResponse?.ER ?? null_flag,
                Rusle2ER = rusle2Response?.ER ?? null_flag,
                WepsStir = wepsResponse.Stir,
                WeppStir = weppResponse?.Stir ?? null_flag,
                Rusle2Stir = rusle2Response?.Stir ?? null_flag,
                WepsWindErosion = wepsResponse.WindErosion,
                WeppSoilLoss = weppResponse?.SoilLoss ?? null_flag,
                Rusle2SoilLoss = rusle2Response?.SoilLoss ?? null_flag,
                Rusle2Sci = rusle2Response?.SCI ?? null_flag,
                WepsAverageBiomass = wepsResponse?.AverageBiomass ?? null_flag
            };

            return result;
        }
    }
}
