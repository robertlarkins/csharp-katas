using Larkins.CSharpKatas.BlobNumbering;

namespace Larkins.CSharpKatas.Tests.Unit.BlobNumbering;

public class BlobDetection2Tests
{
    [Fact]
    public void EmptyImageHasNoBlobs()
    {
        // Arrange
        var stringImage = new[]
        {
            "00000",
            "00000",
            "00000",
            "00000",
            "00000"
        };

        var expectedStringImage = new[]
        {
            "00000",
            "00000",
            "00000",
            "00000",
            "00000"
        };

        var image = StringImageToRectangularIntArray(stringImage);
        var expected = StringImageToRectangularIntArray(expectedStringImage);

        var sut = new BlobDetection2(image);

        // Act
        var result = sut.GetBlobMap();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ImageWithSingleBlobHasBlobLabelledWith1()
    {
        // Arrange
        var stringImage = new[]
        {
            "0000",
            "0110",
            "0110",
            "0000"
        };

        var expectedStringImage = new[]
        {
            "0000",
            "0110",
            "0110",
            "0000"
        };

        var image = StringImageToRectangularIntArray(stringImage);
        var expected = StringImageToRectangularIntArray(expectedStringImage);

        var sut = new BlobDetection2(image);

        // Act
        var result = sut.GetBlobMap();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ImageWithTwoBlobsHasEachBlobUniquelyNumbered()
    {
        // Arrange
        var stringImage = new[]
        {
            "0000110",
            "0110110",
            "0110110",
            "0110110",
            "0000110"
        };

        var expectedStringImage = new[]
        {
            "0000110",
            "0220110",
            "0220110",
            "0220110",
            "0000110"
        };

        var image = StringImageToRectangularIntArray(stringImage);
        var expected = StringImageToRectangularIntArray(expectedStringImage);

        var sut = new BlobDetection2(image);

        // Act
        var result = sut.GetBlobMap();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ImageWithThreeBlobsHasEachBlobUniquelyNumbered()
    {
        // Arrange
        var stringImage = new[]
        {
            "0000110",
            "0110010",
            "0001010",
            "1100010",
            "0110110"
        };

        var expectedStringImage = new[]
        {
            "0000110",
            "0220010",
            "0002010",
            "3300010",
            "0330110"
        };

        var image = StringImageToRectangularIntArray(stringImage);
        var expected = StringImageToRectangularIntArray(expectedStringImage);

        var sut = new BlobDetection2(image);

        // Act
        var result = sut.GetBlobMap();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void SimpleImageWithConcavityIsIdentifiedAsSingleBlob()
    {
        // Arrange
        var stringImage = new[]
        {
            "10101",
            "10001",
            "01110"
        };

        var expectedStringImage = new[]
        {
            "10201",
            "10001",
            "01110"
        };

        var image = StringImageToRectangularIntArray(stringImage);
        var expected = StringImageToRectangularIntArray(expectedStringImage);

        var sut = new BlobDetection2(image);

        // Act
        var result = sut.GetBlobMap();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ImageWithConcavityIsIdentifiedAsSingleBlob()
    {
        // Arrange
        var stringImage = new[]
        {
            "11011011",
            "11011011",
            "11000011",
            "11111111",
            "01111110"
        };

        var expectedStringImage = new[]
        {
            "11022011",
            "11022011",
            "11000011",
            "11111111",
            "01111110"
        };

        var image = StringImageToRectangularIntArray(stringImage);
        var expected = StringImageToRectangularIntArray(expectedStringImage);

        var sut = new BlobDetection2(image);

        // Act
        var result = sut.GetBlobMap();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ImageWithMultipleConcavitiesIsIdentifiedAsSingleBlob()
    {
        // Arrange
        var stringImage = new[]
        {
            "11011011",
            "11011011",
            "11000100",
            "11011011",
            "11011011"
        };

        var expectedStringImage = new[]
        {
            "11022022",
            "11022022",
            "11000200",
            "11022022",
            "11022022"
        };

        var image = StringImageToRectangularIntArray(stringImage);
        var expected = StringImageToRectangularIntArray(expectedStringImage);

        var sut = new BlobDetection2(image);

        // Act
        var result = sut.GetBlobMap();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    private static int[,] StringImageToRectangularIntArray(string[] stringImage)
    {
        if (stringImage.Length == 0)
        {
            return new int[0, 0];
        }

        var height = stringImage.Length;
        var width = stringImage[0].Length;

        var intImage = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                intImage[y, x] = int.Parse(stringImage[y][x].ToString());
            }
        }

        return intImage;
    }
}
