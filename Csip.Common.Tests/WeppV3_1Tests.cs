using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Csip.Common.Tests
{
    public class WeppV3_1Tests
    {
        [Fact]
        public void ParseResults_ValidJson_ExpectedResults()
        {
            // Arrange
            var sut = new WeppV3_1();
            string json = File.ReadAllText(
                @"Assets\exampleWeppResult.json");

            // Act
            WeppResponseV3_1 actual = sut.ParseResultsJson(json);

            // Assert
            Assert.Equal(0.002899999963119626, actual.SoilLoss);
            Assert.Equal("Transition_NoTill", actual.RotationName);
            Assert.Equal("8af8f7ab-064a-11ea-a464-7b605541b058", actual.Suid);
        }
    }
}
