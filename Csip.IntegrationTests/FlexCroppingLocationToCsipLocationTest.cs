using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Helpers;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System.IO;
using System.Net.Http;
using Xunit;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.IntegrationTests
{
    public class FlexCroppingLocationToCsipLocationTest
    {
        [Fact]
        public async void Run_ValidInput_ExpectedResults()
        {
            // Arrange
            var sut = new FlexCroppingLocationToCsipLocation(
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
