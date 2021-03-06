﻿using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario.Tests
{
    public class SciBuilderTest
    {
        [Fact]
        public void BuildScenario_ValidInput_ExpectedResults()
        {
            // Arrange
            IBuildSciModel sut = new SciBuilderWepp();
            var input = GetErosionParametersMockValid();

            // Act
            var actual = sut.BuildScenarios(
                input,
                sut.GetTemplate());

            // Assert
            Assert.Single(actual);
            Assert.Contains("47.053055__-117.24074__Transition_NoTill", actual.First());

        }

        private List<ErosionParameters> GetErosionParametersMockValid()
        {
            List<ErosionParameters> parameters =
                new List<ErosionParameters>()
                {
                    new ErosionParameters()
                    {
                        Latitude = 47.053055,
                        Longitude = -117.24074,
                        RotationName = "Transition_NoTill",
                        WeppER = 0.998857975006103,
                        WeppFO = 0.840264022350311,
                        WeppOM = 1.05330002307891,
                        WepsER = 1,
                        WepsFO = 0.840264022350311,
                        WepsOM = 1.05330002307891
                    }
                };

            return parameters;
        }
    }
}
