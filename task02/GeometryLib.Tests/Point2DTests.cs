namespace GeometryLib.Tests;

public class Point2DTests
{
    [Theory]
    [MemberData(nameof(DistanceToTestData))]
    public void DistanceTo_ShouldReturnCorrectDistance(Point2D a, Point2D b, double expected)
    {
        // Act
        double result = a.DistanceTo(b);

        // Assert
        Assert.Equal(expected, result, Point2D.Precision);
    }

    public static TheoryData<Point2D, Point2D, double> DistanceToTestData()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            { new Point2D(0, 0), new Point2D(3, 4), 5 },
            { new Point2D(1, 1), new Point2D(4, 5), 5 },
            { new Point2D(-1, -1), new Point2D(2, 3), 5 },
            { new Point2D(0, 0), new Point2D(0, 0), 0 },
            { new Point2D(5, 5), new Point2D(5, 5), 0 },
        };
    }

    [Theory]
    [MemberData(nameof(EqualityTestData))]
    public void Equals_ShouldReturnCorrectResult(Point2D a, Point2D b, bool expected)
    {
        // Act & Assert
        Assert.Equal(expected, a.Equals(b));
        Assert.Equal(expected, b.Equals(a));
        Assert.Equal(expected, a == b);
        Assert.Equal(!expected, a != b);
    }

    public static TheoryData<Point2D, Point2D, bool> EqualityTestData()
    {
            return new TheoryData<Point2D, Point2D, bool>
        {
            { new Point2D(1, 2), new Point2D(1, 2), true },
            { new Point2D(1.00000000001, 2), new Point2D(1, 2.00000000001), true }, // в пределах допуска
            { new Point2D(1, 2), new Point2D(1.1, 2), false }, // за пределами допуска
            { new Point2D(1, 2), new Point2D(1, 2.1), false }, // за пределами допуска
        };
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValueForEqualPoints()
    {
        // Arrange
        Point2D point1 = new Point2D(1.23456789, 9.87654321);
        Point2D point2 = new Point2D(1.23456789, 9.87654321);

        // Act & Assert
        Assert.Equal(point1.GetHashCode(), point2.GetHashCode());
    }
}