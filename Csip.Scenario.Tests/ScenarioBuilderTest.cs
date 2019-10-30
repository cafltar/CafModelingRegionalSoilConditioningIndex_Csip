using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario.Tests
{
    public class ScenarioBuilderTest
    {
        [Fact]
        public void Build_ValidInput_ExpectedResults()
        {
            // Arrange
            ScenarioBuilder sut = new ScenarioBuilder();

            // Act
            string actual = sut.Build();

            // Assert
            Assert.NotNull(actual);
        }
    }
}
