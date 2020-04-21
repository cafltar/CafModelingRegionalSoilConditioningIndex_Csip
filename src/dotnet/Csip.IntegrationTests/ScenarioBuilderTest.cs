using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Csip.IntegrationTests
{

    public class ScenarioBuilderTest
    {
        [Fact]
        public void Wepp_Build_ValidInput01Loc_ExpectedResults()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IBuildErosionModel sut = new WeppBuilder();

            // Act
            List<string> actual = sut.BuildScenarios(
                reader.ReadCsipLocationFile(@"Assets\location_verification_01.csv"),
                sut.GetTemplate(),
                sut.GetRotations());

            // Assert
            string strippedJson = Regex.Replace(actual.FirstOrDefault(), @"\s+", "");

            // Check soil cokey, soil slope, soil length, rotation soil length
            Assert.Contains("{\"name\":\"soilPtr\",\"value\":[\"17389235\"]}", strippedJson);
            Assert.Contains("{\"name\":\"slope_steepness\",\"value\":4.0}", strippedJson);
            Assert.Contains("{\"name\":\"length\",\"value\":350.0}", strippedJson);
            Assert.Contains("{\"duration\":2,\"length\":350.0", strippedJson);
        }

        [Fact]
        public void Weps_Build_ValidInput01Loc_ExpectedResult()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IBuildErosionModel sut = new WepsBuilder();

            // Act
            List<string> actual = sut.BuildScenarios(
                reader.ReadCsipLocationFile(@"Assets\location_verification_01.csv"),
                sut.GetTemplate(),
                sut.GetRotations());

            // Assert
            string strippedJson = Regex.Replace(actual.FirstOrDefault(), @"\s+", "");
            // assert rectangle lengths, others?
            Assert.Contains("{\"duration\":2,\"length\":350.0", strippedJson);
        }

        [Fact]
        public void Rusle2_Build_ValidInput01Loc_ExpectedResult()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IBuildErosionModel sut = new Rusle2Builder();

            // Act
            List<string> actual = sut.BuildScenarios(
                reader.ReadCsipLocationFile(@"Assets\location_verification_01.csv"),
                sut.GetTemplate(),
                sut.GetRotations());

            // Assert
            string strippedJson = Regex.Replace(actual.FirstOrDefault(), @"\s+", "");

            // Check soil cokey, soil slope, soil length, rotation name
            Assert.Contains("{\"name\":\"soilPtr\",\"value\":[\"17389235\"]}", strippedJson);
            Assert.Contains("{\"name\":\"steepness\",\"value\":4.0}", strippedJson);
            Assert.Contains("{\"name\":\"length\",\"value\":350.0}", strippedJson);
            Assert.Contains("\"name\":\"R2_GrainFallow_HeavyTillage\"", strippedJson);
        }

        [Fact]
        public void Wepp_Build_ValidInput10Locs_ExpectedResults()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IBuildErosionModel sut = new WeppBuilder();

            // Act
            List<string> actual = sut.BuildScenarios(
                reader.ReadCsipLocationFile(@"Assets\location_verification_10.csv"),
                sut.GetTemplate(),
                sut.GetRotations());

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(33, actual.Count);
        }

        [Fact]
        public void Weps_Build_ValidInput10Locs_ExpectedResult()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IBuildErosionModel sut = new WepsBuilder();

            // Act
            List<string> actual = sut.BuildScenarios(
                reader.ReadCsipLocationFile(@"Assets\location_verification_10.csv"),
                sut.GetTemplate(),
                sut.GetRotations());

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(33, actual.Count);
        }

        [Fact]
        public void Weps_WriteScenarioFiles_FromVerificationLocations()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IBuildErosionModel builder = new WepsBuilder();
            ScenarioHandler writer = new ScenarioHandler();
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string expectedZip = $"Assets\\output\\weps_{currentDate}.zip";
            string writePath = $"Assets\\output\\weps_{currentDate}";

            // Act
            List<string> actual = builder.BuildScenarios(
                reader.ReadCsipLocationFile(@"Assets\location_verification_10.csv"),
                builder.GetTemplate(),
                builder.GetRotations());

            writer.WriteScenariosZip(actual, writePath);

            // Assert
            Assert.True(File.Exists(expectedZip));

            // Cleanup
            if (File.Exists(expectedZip))
                File.Delete(expectedZip);
            if (Directory.Exists(writePath))
                Directory.Delete(writePath, true);
        }

        [Fact]
        public void Wepp_WriteScenarioFiles_FromVerificationLocations()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IBuildErosionModel builder = new WeppBuilder();
            ScenarioHandler writer = new ScenarioHandler();
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string expectedZip = $"Assets\\output\\wepp_{currentDate}.zip";
            string writePath = $"Assets\\output\\wepp_{currentDate}";

            // Act
            List<string> actual = builder.BuildScenarios(
                reader.ReadCsipLocationFile(@"Assets\location_verification_10.csv"),
                builder.GetTemplate(),
                builder.GetRotations());

            writer.WriteScenariosZip(actual, writePath);

            // Assert
            Assert.True(File.Exists(expectedZip));

            // Cleanup
            if (File.Exists(expectedZip))
                File.Delete(expectedZip);
            if (Directory.Exists(writePath))
                Directory.Delete(writePath, true);
        }

        [Fact]
        public void SciBuilder_WritesScenarioFiles_FromVerificationLocations()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            SciBuilder builder = new SciBuilder();
            ScenarioHandler writer = new ScenarioHandler();
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string writePath = $"Assets\\output\\sci_{currentDate}";
            string expectedZip = $"Assets\\output\\sci_{currentDate}.zip";

            // Act
            List<string> actual = builder.BuildScenarios(
                reader.ReadErosionParameters(@"Assets\erosion-params-output.csv"),
                builder.GetTemplate());

            writer.WriteScenariosZip(actual, writePath);

            // Assert
            Assert.True(File.Exists(expectedZip));

            // Cleanup
            if (File.Exists(expectedZip))
                File.Delete(expectedZip);
            if (Directory.Exists(writePath))
                Directory.Delete(writePath, true);
        }
    }
}
