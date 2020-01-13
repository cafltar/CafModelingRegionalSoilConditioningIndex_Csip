using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Csip.Common.Tests
{
    public class WepsV5_2Tests
    {
        [Fact]
        public void ParseResults_ValidJson_ExpectedResults()
        {
            // Arrange
            var sut = new WepsV5_2();
            string json = File.ReadAllText(
                @"Assets\exampleWepsResultV5_2.json");

            // Act
            WepsResponseV5_2 actual = sut.ParseResultsJson(json);

            // Assert
            Assert.Equal("95abb930-3246-11ea-bc15-6386ee4990d5", actual.Suid);
            Assert.Equal("GrainFallow_HeavyTillage", actual.RotationName);
            Assert.Equal(0.223490870061, actual.WindErosion);
            Assert.Equal(-0.1239, actual.OM);
            Assert.Equal(0.9119, actual.ER);
            Assert.Equal(0.0114, actual.FO);
            Assert.Equal(99.845, actual.Stir);
        }
    }
}
