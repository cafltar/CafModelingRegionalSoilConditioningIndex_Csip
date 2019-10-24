using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Cokey;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Cokey.Tests
{
    public class PointToPolygonConverterTests
    {
        [Fact]
        public void GetPixelAsBoundingBoxString_ValidInput_ReturnsExpected()
        {
            // Arrange
            var sut = new PointToPolygonConverter();
            string expected = "[[-118.67213507701905,45.71042895488162],[-118.67213507701905,45.74640181911838],[-118.62060247098096,45.74640181911838],[-118.62060247098096,45.71042895488162],[-118.67213507701905,45.71042895488162]]";

            // Act
            string actual = sut.GetPixelAsBoundingBoxString(
                45.72841538700,
                -118.64636877400,
                4);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
