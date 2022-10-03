using AngularUrlShortener.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularUrlShortener.Tests
{
    public class RandomCodeGeneratorTests
    {
        [Fact]
        public void CanGenerateRandomString()
        {
            // Arrange
            // Act
            string result = RandomCodeGenerator.GetRand(6);

            // Assert
            Assert.True(result.Length == 6);
        }
    }
}
