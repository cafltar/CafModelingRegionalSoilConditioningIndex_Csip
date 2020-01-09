using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Csip.Common.Tests
{
    public class SciV2_1Tests
    {
        [Fact]
        public void ParseResults_ValidJson_ExpectedResults()
        {
            // Arrange
            var sut = new SciV2_1();
            string json = File.ReadAllText(
                @"Assets\exampleSciResponseV2_1.json");

            // Act
            SciResponseV2_1 actual = sut.ParseResultsJson(json);

            // Assert
            Assert.Equal(45.0001, actual.Latitude);
            Assert.Equal(-117.3001, actual.Longitude);
            Assert.Equal("Annual_NoTill", actual.RotationName);
            Assert.Equal(0.2, actual.SciTotal);
        }
    }
}
