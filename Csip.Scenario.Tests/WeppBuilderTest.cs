using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario.Tests
{
    public class WeppBuilderTest
    {
        [Fact]
        public void GetRotations_ValidInput_ExpectedResults()
        {
            // Arrange
            IBuildErosionModel sut = new WeppBuilder();

            // Act
            var actual = sut.GetRotations();

            // Assert
            Assert.Equal(6, actual.Count);
            Assert.Equal(4, actual[11].Count);
            Assert.Equal(3, actual[112].Count);
        }

        [Fact]
        public void Build_ValidInput_ExpectedResults()
        {
            // Arrange
            IBuildErosionModel sut = new WeppBuilder();
            List<CsipLocation> locations = GetMockLocations();
            string template = File.ReadAllText(@"Assets\templateWepp.json");
            Dictionary<int, List<string>> rotations = GetMockRotations();

            // Act
            List<string> actual = sut.BuildScenarios(
                locations,
                template,
                rotations);

            // Assert
            Assert.Equal(20, actual.Count);
        }

        private List<CsipLocation> GetMockLocations()
        {
            List<CsipLocation> locations = new List<CsipLocation>()
            {
                new CsipLocation()
                {
                    Cokey = "1000",
                    Latitude = 45.0,
                    Longitude = -117.0,
                    AnthromeKey = 11
                },
                new CsipLocation()
                {
                    Cokey = "1001",
                    Latitude = 45.1,
                    Longitude = -117.1,
                    AnthromeKey = 111
                },
                new CsipLocation()
                {
                    Cokey = "2000",
                    Latitude = 46.0,
                    Longitude = -118.0,
                    AnthromeKey = 12
                },
                new CsipLocation()
                {
                    Cokey = "2001",
                    Latitude = 46.1,
                    Longitude = -118.1,
                    AnthromeKey = 112
                },
                new CsipLocation()
                {
                    Cokey = "3000",
                    Latitude = 47.0,
                    Longitude = -119.0,
                    AnthromeKey = 13
                },
                new CsipLocation()
                {
                    Cokey = "3001",
                    Latitude = 47.1,
                    Longitude = -119.1,
                    AnthromeKey = 113
                }
            };

            return locations;
        }

        private Dictionary<int, List<string>> GetMockRotations()
        {
            Dictionary<int, List<string>> rotations =
                new Dictionary<int, List<string>>()
                {
                    {   
                        11,
                        new List<string>()
                        {
                            "{\"rot\":\"Annual_HeavyTillage\"}",
                            "{\"rot\":\"Annual_NoTill\"}",
                            "{\"rot\":\"Annual_ReducedTill\"}",
                            "{\"rot\":\"Annual_Preventative\"}"
                        }
                    },
                    {
                        111,
                        new List<string>()
                        {
                            "{\"rot\":\"Annual_HeavyTillage\"}",
                            "{\"rot\":\"Annual_NoTill\"}",
                            "{\"rot\":\"Annual_ReducedTill\"}",
                            "{\"rot\":\"Annual_Preventative\"}"
                        }
                    },
                    {
                        12,
                        new List<string>()
                        {
                            "{\"rot\":\"Transition_HeavyTillage\"}",
                            "{\"rot\":\"Transition_NoTill\"}",
                            "{\"rot\":\"Transition_ReducedTill\"}"
                        }
                    },
                    {
                        112,
                        new List<string>()
                        {
                            "{\"rot\":\"Transition_HeavyTillage\"}",
                            "{\"rot\":\"Transition_NoTill\"}",
                            "{\"rot\":\"Transition_ReducedTill\"}"
                        }
                    },
                    {
                        13,
                        new List<string>()
                        {
                            "{\"rot\":\"GrainFallow_HeavyTillage\"}",
                            "{\"rot\":\"GrainFallow_NoTill\"}",
                            "{\"rot\":\"GrainFallow_ReducedTill\"}"
                        }
                    },
                    {
                        113,
                        new List<string>()
                        {
                            "{\"rot\":\"GrainFallow_HeavyTillage\"}",
                            "{\"rot\":\"GrainFallow_NoTill\"}",
                            "{\"rot\":\"GrainFallow_ReducedTill\"}"
                        }
                    }
                };

            return rotations;
        }
    }
}
