using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using System;
using System.IO;
using System.Net.Http;
using Xunit;

namespace Csip.Common.Tests
{
    public class WweSoilParamsV2_0Tests
    {
        readonly string jsonPolygonValid = "[[-118.67213507701905,45.71042895488162],[-118.67213507701905,45.74640181911838],[-118.62060247098096,45.74640181911838],[-118.62060247098096,45.71042895488162],[-118.67213507701905,45.71042895488162]]";
        HttpClient client = new HttpClient();

        [Fact]
        public void BuildJsonContent_ValidJson_ExpectedResult()
        {
            // Arrange
            //HttpClient client = new HttpClient();
            var sut = new WweSoilParamsV2_0(client);

            string expected = @"{""metainfo"":{},""parameter"":[{""name"":""aoa_geometry"",""type"":""Polygon"",""coordinates"":[[[-118.67213507701905,45.71042895488162],[-118.67213507701905,45.74640181911838],[-118.62060247098096,45.74640181911838],[-118.62060247098096,45.71042895488162],[-118.67213507701905,45.71042895488162]]]}]}";

            // Act
            string actual = sut.BuildJsonContent(jsonPolygonValid);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Post_ValidJson_Success()
        {
            // Arrange
            var sut = new WweSoilParamsV2_0(client);

            // Act
            string actual = await sut.Post(jsonPolygonValid);

            // Assert
            Assert.True(!String.IsNullOrEmpty(actual));
        }

        [Fact]
        public void ParseResults_ValidJson_ExpectedResult()
        {
            // Arrange
            var sut = new WweSoilParamsV2_0(client);
            string json = File.ReadAllText(
                @"Assets\WweSoilParamsV2JsonResults.json");
            //WweSoilParamsV2Results expected = new WweSoilParamsV2Results();

            // Act
            WweSoilParamsV2Results actual = sut.ParseResultsJson(json);

            // Assert
            Assert.Equal(10, actual.MapUnits.Count);
            Assert.Equal("3963.178", actual.MapUnitMeta.Area);
        }
    }
}
