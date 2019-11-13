using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Cokey;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.IntegrationTests
{
    public class Cokey_OrchestratorTests
    {
        [Fact]
        public async void Run_ValidInput_ExpectedResults()
        {
            // Arrange
            var sut = new Orchestrator(
                new CsvHandler(),
                new WweSoilParamsV2_0(new HttpClient()),
                new PointToPolygonConverter(),
                new CokeyChooser());
            string inputFile = @"Assets\verification_10.csv";
            string outputFile = @"Assets\locations-output.csv";

            // Act
            await sut.Run(inputFile, outputFile);

            // Assert
            Assert.True(File.Exists(outputFile));

            if (File.Exists(outputFile))
                File.Delete(outputFile);
        }
    }
}
