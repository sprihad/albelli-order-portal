using AlbelliOrderPortal.Helpers;
using System;
using Xunit;

namespace AlbelliOrderPortalTest
{
    public class BinWidthComputationTest
    {
        [Fact]
        public void WidthCalculator_Returns_RequiredBinWidth()
        {
            //Arrange
            var productType = ProductType.Mug;
            var quantity = 9;

            //Act
            var requiredBinWidth = BinWidthCompuation.ComputeBinWidth(productType, quantity);

            //Assert
            Assert.Equal(282, requiredBinWidth);
        }

        [Fact]
        public void WidthCalculator_ReturnsZero_When_QuantityisZero()
        {
            //Arrange
            var productType = ProductType.PhotoBook;
            var quantity = 0;

            //Act
            var requiredBinWidth = BinWidthCompuation.ComputeBinWidth(productType, quantity);

            //Assert
            Assert.True(requiredBinWidth == 0, "BinWidth is zero when quantity is zero");
        }

        [Fact]
        public void WidthCalculator_ThrowsException_ForUnimplemented_ProductType()
        {
            //Arrange
            var productType = (ProductType)5;
            var quantity = 3;

            //Assert
            Assert.Throws<NotImplementedException>(() => BinWidthCompuation.ComputeBinWidth(productType, quantity));
        }
    }
}
