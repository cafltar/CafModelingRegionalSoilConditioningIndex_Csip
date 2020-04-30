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
            Assert.Equal(46.311713123, actual.Latitude);
            Assert.Equal(-116.896475566, actual.Longitude);
            Assert.Equal("Transition_NoTill", actual.RotationName);
            Assert.Equal(0.6309946026996589, actual.SciTotal);
            Assert.Equal(0.970992957076145, actual.ErosionWater);
            Assert.Equal(0.717950513657798, actual.WaterOM);
            Assert.Equal(0.734653465346535, actual.WaterFO);
            Assert.Equal(0.717950513657798, actual.WindOM);
            Assert.Equal(0.7604, actual.WindFO);
            Assert.Equal(1, actual.ErosionWind);
        }
    }
}
