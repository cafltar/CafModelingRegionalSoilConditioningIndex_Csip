using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Cokey;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Helpers;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Cokey.Tests
{
    public class CokeyChooserTests
    {
        [Fact]
        public void GetDominateCokey_ValidInput_ReturnsExpected()
        {
            // Arrange
            var sut = new CokeyChooser();
            string expected = "17391360";
            WweSoilParamsResponseV2_0 mockResults = GetMockWweSoilParamsV2Results();

            // Act
            string actual = sut.GetDominateCokey(mockResults);

            // Assert
            Assert.Equal(expected, actual);
        }

        private WweSoilParamsResponseV2_0 GetMockWweSoilParamsV2Results()
        {
            WweSoilParamsResponseV2_0 results = new WweSoilParamsResponseV2_0()
            {
                MapUnitMeta = new MapUnitMeta()
                {
                    Area = "3963.178",
                    Name = "AoA Area",
                    Units = "acres"
                },
                MapUnits = new List<MapUnit>()
                {
                    new MapUnit()
                    {
                        Area = 1589.15,
                        Mukey = "64429",
                        Name = "Walla Walla silt loam, 1 to 7 percent slopes",
                        Components = new List<Component>()
                        {
                            new Component()
                            {
                                Cokey = "17391360",
                                Name = "Walla Walla",
                                PercentOfMapUnit = 85
                            }
                        }
                    },
                    new MapUnit()
                    {
                        Area = 276.694,
                        Mukey = "64430",
                        Name = "Walla Walla silt loam, 7 to 12 percent slopes",
                        Components = new List<Component>()
                        {
                            new Component()
                            {
                                Cokey = "17391361",
                                Name = "Walla Walla",
                                PercentOfMapUnit = 75
                            }
                        }
                    },
                    new MapUnit()
                    {
                        Area = 1.249,
                        Mukey = "64431",
                        Name = "Walla Walla silt loam, 12 to 25 percent north slopes",
                        Components = new List<Component>()
                        {
                            new Component()
                            {
                                Cokey = "17391362",
                                Name = "Walla Walla",
                                PercentOfMapUnit = 75
                            }
                        }
                    }
                }
            };

            return results;
        }
    }
}
