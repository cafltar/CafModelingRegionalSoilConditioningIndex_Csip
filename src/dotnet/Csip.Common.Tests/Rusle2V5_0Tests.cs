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
            Assert.Equal("afb894a2-83ff-11ea-a7e9-45c43e9e1c1d", actual.Suid);
            Assert.Equal(0.81081946920465, actual.SegSoilLoss);
        }
    }
}
