using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Csip.Common.Tests
{
    public class CsvHandlerTests
    {
        [Fact]
        public void ReadFlexCroppingLocationFile_ValidFile_ExpectedResult()
        {
            // Arrange
            var sut = new CsvHandler();
            string filePath = @"Assets\verification_10.csv";

            // Act
            List<FlexCroppingLocation> actual = 
                sut.ReadFlexCroppingLocationFile(filePath);

            // Assert
            Assert.Equal(10, actual.Count);
        }
    }
}
