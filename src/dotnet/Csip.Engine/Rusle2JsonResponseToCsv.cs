using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine
{
    public class Rusle2JsonResponseToCsv
    {
        private readonly JsonHandler jsonHandler;
        private readonly Rusle2V5_0 serviceHandler;
        private readonly CsvHandler csvHandler;

        public Rusle2JsonResponseToCsv(
            JsonHandler jsonHandler,
            Rusle2V5_0 serviceHandler,
            CsvHandler csvHandler)
        {
            this.jsonHandler = jsonHandler;
            this.serviceHandler = serviceHandler;
            this.csvHandler = csvHandler;
        }

        public bool Run(
            string inputRusle2Path,
            string outputFilePath)
        {
            // Read json files with simulation results
            List<Rusle2ResponseV5_0> rusle2Responses =
                jsonHandler.ReadRusle2ResponseV5_0Files(
                    inputRusle2Path, this.serviceHandler);

            // Write files
            csvHandler.WriteRusle2ResultFile(
                outputFilePath,
                rusle2Responses);

            return true;
        }
    }
}
