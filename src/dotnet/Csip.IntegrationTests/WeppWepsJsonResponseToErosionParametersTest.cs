using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Csip.IntegrationTests
{
    public class ErosionModelJsonResponseToErosionParametersTest
    {
        [Fact]
        public void Run_ValidInput_ExpectedResults()
        {
            // Arrange
            var sut = new ErosionModelJsonResponseToErosionParameters(
                new JsonHandler(),
                new WeppV3_1(),
                new WepsV5_2(),
                new Rusle2V5_0(),
                new CsvHandler());
            string inputWepp = @"Assets\weppResponsesValid";
            string inputWeps = @"Assets\wepsResponsesValid";
            string inputRusle2 = @"Assets\rusle2ResponsesValid";
            string outputFile = @"Assets\erosion-params-output.csv";

            // Act
            sut.Run(inputWepp, inputWeps, inputRusle2, outputFile);

            // Assert
            Assert.True(File.Exists(outputFile));

            if (File.Exists(outputFile))
                File.Delete(outputFile);
        }
    }
}
