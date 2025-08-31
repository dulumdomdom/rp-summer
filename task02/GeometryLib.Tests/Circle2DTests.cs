namespace GeometryLib.Tests;

public class Circle2DTests
{
    [Fact]
    public void Constructor_WithZeroRadius_ShouldThrowArgumentException()
    {
        // Arrange
        Point2D center = new Point2D(0, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Circle2D(center, 0));
    }

    [Fact]
    public void Constructor_WithNegativeRadius_ShouldThrowArgumentException()
    {
        // Arrange
        Point2D center = new Point2D(0, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Circle2D(center, -1));
    }

    [Theory]
    [MemberData(nameof(CirclePropertiesTestData))]
    public void Properties_ShouldReturnCorrectValues(Point2D center, double radius, double expectedDiameter, double expectedCircumference, double expectedArea)
    {
        // Arrange
        Circle2D circle = new Circle2D(center, radius);

        // Act & Assert
        Assert.Equal(expectedDiameter, circle.Diameter, Circle2D.Precision);
        Assert.Equal(expectedCircumference, circle.Circumference, Circle2D.Precision);
        Assert.Equal(expectedArea, circle.Area, Circle2D.Precision);
    }

    public static TheoryData<Point2D, double, double, double, double> CirclePropertiesTestData()
    {
        return new TheoryData<Point2D, double, double, double, double>
        {
            { new Point2D(0, 0), 1, 2, 2 * Math.PI, Math.PI },
            { new Point2D(0, 0), 2, 4, 4 * Math.PI, 4 * Math.PI },
            { new Point2D(0, 0), 0.5, 1, Math.PI, 0.25 * Math.PI },
        };
    }

    [Theory]
    [MemberData(nameof(DistanceToPointTestData))]
    public void DistanceToPoint_ShouldReturnCorrectDistance(Point2D center, double radius, Point2D point, double expected)
    {
        // Arrange
        Circle2D circle = new Circle2D(center, radius);

        // Act
        double result = circle.DistanceToPoint(point);

        // Assert
        Assert.Equal(expected, result, Circle2D.Precision);
    }

    public static TheoryData<Point2D, double, Point2D, double> DistanceToPointTestData()
    {
        return new TheoryData<Point2D, double, Point2D, double>
        {
            { new Point2D(0, 0), 1, new Point2D(2, 0), 1 },
            { new Point2D(0, 0), 1, new Point2D(0.5, 0), 0.5 },
            { new Point2D(0, 0), 1, new Point2D(0, 0), 1 },
            { new Point2D(0, 0), 1, new Point2D(1, 0), 0 },
        };
    }

    [Theory]
    [MemberData(nameof(DistanceToCircleTestData))]
    public void DistanceToCircle_ShouldReturnCorrectDistance(Point2D center1, double radius1, Point2D center2, double radius2, double expected)
    {
        // Arrange
        Circle2D circle1 = new Circle2D(center1, radius1);
        Circle2D circle2 = new Circle2D(center2, radius2);

        // Act
        double result = circle1.DistanceToCircle(circle2);

        // Assert
        Assert.Equal(expected, result, Circle2D.Precision);
    }

    public static TheoryData<Point2D, double, Point2D, double, double> DistanceToCircleTestData()
    {
        return new TheoryData<Point2D, double, Point2D, double, double>
        {
            { new Point2D(0, 0), 1, new Point2D(3, 0), 1, 1 },
            { new Point2D(0, 0), 1, new Point2D(2, 0), 1, 0 },
            { new Point2D(0, 0), 1, new Point2D(1, 0), 1, 0 },
            { new Point2D(0, 0), 2, new Point2D(1, 0), 1, 0 },
        };
    }

    [Theory]
    [MemberData(nameof(ContainsPointTestData))]
    public void ContainsPoint_ShouldReturnCorrectResult(Point2D center, double radius, Point2D point, bool expected)
    {
        // Arrange
        Circle2D circle = new Circle2D(center, radius);

        // Act
        bool result = circle.ContainsPoint(point);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<Point2D, double, Point2D, bool> ContainsPointTestData()
    {
        return new TheoryData<Point2D, double, Point2D, bool>
        {
            { new Point2D(0, 0), 1, new Point2D(0, 0), true },
            { new Point2D(0, 0), 1, new Point2D(0.5, 0.5), true },
            { new Point2D(0, 0), 1, new Point2D(1, 0), true },
            { new Point2D(0, 0), 1, new Point2D(1.1, 0), false },
            { new Point2D(0, 0), 1, new Point2D(0, 1.1), false },
        };
    }

    [Theory]
    [MemberData(nameof(IntersectsWithTestData))]
    public void IntersectsWith_ShouldReturnCorrectResult(Point2D center1, double radius1, Point2D center2, double radius2, bool expected)
    {
        // Arrange
        Circle2D circle1 = new Circle2D(center1, radius1);
        Circle2D circle2 = new Circle2D(center2, radius2);

        // Act
        bool result = circle1.IntersectsWith(circle2);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<Point2D, double, Point2D, double, bool> IntersectsWithTestData()
    {
        return new TheoryData<Point2D, double, Point2D, double, bool>
        {
            { new Point2D(0, 0), 1, new Point2D(3, 0), 1, false },
            { new Point2D(0, 0), 1, new Point2D(2, 0), 1, true },
            { new Point2D(0, 0), 1, new Point2D(1, 0), 1, true },
            { new Point2D(0, 0), 2, new Point2D(1, 0), 1, true },
            { new Point2D(0, 0), 1, new Point2D(0, 0), 1, true },
        };
    }

    [Theory]
    [MemberData(nameof(ContainsCircleTestData))]
    public void ContainsCircle_ShouldReturnCorrectResult(Point2D center1, double radius1, Point2D center2, double radius2, bool expected)
    {
        // Arrange
        Circle2D circle1 = new Circle2D(center1, radius1);
        Circle2D circle2 = new Circle2D(center2, radius2);

        // Act
        bool result = circle1.ContainsCircle(circle2);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<Point2D, double, Point2D, double, bool> ContainsCircleTestData()
    {
        return new TheoryData<Point2D, double, Point2D, double, bool>
    {
        { new Point2D(0, 0), 3, new Point2D(0, 0), 1, true }, // маленький круг в центре
        { new Point2D(0, 0), 3, new Point2D(1, 0), 1, true }, // маленький круг смещен
        { new Point2D(0, 0), 3, new Point2D(0, 0), 3, true }, // одинаковые круги
        { new Point2D(0, 0), 3, new Point2D(1, 0), 1.5, true }, // 1 + 1.5 = 2.5 <= 3
        { new Point2D(0, 0), 3, new Point2D(2, 0), 1, true }, // 2 + 1 = 3 <= 3
        { new Point2D(0, 0), 3, new Point2D(2, 0), 1.1, false }, // 2 + 1.1 = 3.1 > 3
        { new Point2D(0, 0), 3, new Point2D(2, 0), 2, false }, // 2 + 2 = 4 > 3
        { new Point2D(0, 0), 1, new Point2D(2, 0), 1, false }, // далеко друг от друга
    };
    }

    [Theory]
    [MemberData(nameof(EqualityTestData))]
    public void Equals_ShouldReturnCorrectResult(Point2D center1, double radius1, Point2D center2, double radius2, bool expected)
    {
        // Arrange
        Circle2D circle1 = new Circle2D(center1, radius1);
        Circle2D circle2 = new Circle2D(center2, radius2);

        // Act & Assert
        Assert.Equal(expected, circle1.Equals(circle2));
        Assert.Equal(expected, circle2.Equals(circle1));
    }

    public static TheoryData<Point2D, double, Point2D, double, bool> EqualityTestData()
    {
        return new TheoryData<Point2D, double, Point2D, double, bool>
        {
            { new Point2D(0, 0), 1, new Point2D(0, 0), 1, true },
            { new Point2D(1, 2), 3, new Point2D(1, 2), 3, true },
            { new Point2D(0, 0), 1, new Point2D(0, 0), 2, false },
            { new Point2D(0, 0), 1, new Point2D(1, 0), 1, false },
        };
    }
}