using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Csip.IntegrationTests
{
    
    public class ScenarioBuilderTest
    {
        [Fact]
        public void Wepp_Build_ValidInput_ExpectedResults()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IScenarioBuilder sut = new WeppBuilder();

            // Act
            List<string> actual = sut.BuildScenarios(
                reader.ReadLocationFile(@"Assets\location_verification_10.csv"),
                sut.GetTemplate("wepp"),
                sut.GetRotations());

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Weps_Build_ValidInput_ExpectedResult()
        {
            // Arrange
            CsvHandler reader = new CsvHandler();
            IScenarioBuilder sut = new WepsBuilder();

            // Act
            List<string> actual = sut.BuildScenarios(
                reader.ReadLocationFile(@"Assets\location_verification_10.csv"),
                sut.GetTemplate(),
                sut.GetRotations());

            // Assert
            Assert.NotNull(actual);
        }
    }
}
