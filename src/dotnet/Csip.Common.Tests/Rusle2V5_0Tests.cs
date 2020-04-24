using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Csip.Common.Tests
{
    public class Rusle2V5_0Tests
    {
        [Fact]
        public void ParseResults_ValidJson_ExpectedResults()
        {
            // Arrange
            var sut = new Rusle2V5_0();
            string json = File.ReadAllText(
                @"Assets\exampleRusle2ResponseV5_0.json");

            // Act
            Rusle2ResponseV5_0 actual = sut.ParseResultsJson(json);

            // Assert
            Assert.Equal(0.188843446136428, actual.SCI);
            Assert.Equal("R2_GrainFallow_HeavyTillage", actual.RotationName);
            Assert.Equal("69abba4e-85b8-11ea-9468-2d456bab2f27", actual.Suid);
            Assert.Equal(0.680779736533603, actual.ER);
            Assert.Equal(0.011386138613861, actual.FO);
            Assert.Equal(0.120332608460407, actual.OM);
        }
    }
}
