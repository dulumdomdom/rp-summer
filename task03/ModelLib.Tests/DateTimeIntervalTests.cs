namespace ModelLib.Tests;

public class DateTimeIntervalTests
{
    [Fact]
    public void Constructor_WithValidDates_ShouldCreateInterval()
    {
        // Arrange
        DateTime start = new DateTime(2024, 1, 1, 10, 0, 0);
        DateTime end = new DateTime(2024, 1, 1, 11, 0, 0);

        // Act
        DateTimeInterval interval = new DateTimeInterval(start, end);

        // Assert
        Assert.Equal(start, interval.Start);
        Assert.Equal(end, interval.End);
        Assert.Equal(TimeSpan.FromHours(1), interval.Duration);
    }

    [Fact]
    public void Constructor_WithEndEqualToStart_ShouldThrowArgumentException()
    {
        // Arrange
        DateTime start = new DateTime(2024, 1, 1, 10, 0, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new DateTimeInterval(start, start));
    }

    [Fact]
    public void Constructor_WithEndBeforeStart_ShouldThrowArgumentException()
    {
        // Arrange
        DateTime start = new DateTime(2024, 1, 1, 10, 0, 0);
        DateTime end = new DateTime(2024, 1, 1, 9, 0, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new DateTimeInterval(start, end));
    }

    [Theory]
    [MemberData(nameof(OverlapsTestData))]
    public void Overlaps_ShouldReturnCorrectResult(DateTimeInterval a, DateTimeInterval b, bool expected)
    {
        // Act
        bool result = a.Overlaps(b);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<DateTimeInterval, DateTimeInterval, bool> OverlapsTestData()
    {
        DateTime baseTime = new DateTime(2024, 1, 1, 10, 0, 0);

        return new TheoryData<DateTimeInterval, DateTimeInterval, bool>
        {
            // Не пересекаются
            {
                new DateTimeInterval(baseTime, baseTime.AddHours(1)),
                new DateTimeInterval(baseTime.AddHours(2), baseTime.AddHours(3)),
                false
            },
            // Пересекаются
            {
                new DateTimeInterval(baseTime, baseTime.AddHours(2)),
                new DateTimeInterval(baseTime.AddHours(1), baseTime.AddHours(3)),
                true
            },
            // Касаются (не пересекаются)
            {
                new DateTimeInterval(baseTime, baseTime.AddHours(1)),
                new DateTimeInterval(baseTime.AddHours(1), baseTime.AddHours(2)),
                false
            },
            // Один внутри другого
            {
                new DateTimeInterval(baseTime, baseTime.AddHours(3)),
                new DateTimeInterval(baseTime.AddHours(1), baseTime.AddHours(2)),
                true
            },
            // Совпадают
            {
                new DateTimeInterval(baseTime, baseTime.AddHours(1)),
                new DateTimeInterval(baseTime, baseTime.AddHours(1)),
                true
            },
        };
    }

    [Theory]
    [MemberData(nameof(CompareToTestData))]
    public void CompareTo_ShouldReturnCorrectResult(DateTimeInterval a, DateTimeInterval b, int expected)
    {
        // Act
        int result = a.CompareTo(b);

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<DateTimeInterval, DateTimeInterval, int> CompareToTestData()
    {
        DateTime baseTime = new DateTime(2024, 1, 1, 10, 0, 0);

        return new TheoryData<DateTimeInterval, DateTimeInterval, int>
        {
            {
                new DateTimeInterval(baseTime, baseTime.AddHours(1)),
                new DateTimeInterval(baseTime.AddHours(1), baseTime.AddHours(2)),
                -1
            },
            {
                new DateTimeInterval(baseTime.AddHours(1), baseTime.AddHours(2)),
                new DateTimeInterval(baseTime, baseTime.AddHours(1)),
                1
            },
            {
                new DateTimeInterval(baseTime, baseTime.AddHours(1)),
                new DateTimeInterval(baseTime, baseTime.AddHours(2)),
                0
            },
        };
    }

    [Fact]
    public void Equality_ShouldWorkCorrectly()
    {
        // Arrange
        DateTime start = new DateTime(2024, 1, 1, 10, 0, 0);
        DateTime end = new DateTime(2024, 1, 1, 11, 0, 0);

        DateTimeInterval interval1 = new DateTimeInterval(start, end);
        DateTimeInterval interval2 = new DateTimeInterval(start, end);
        DateTimeInterval interval3 = new DateTimeInterval(start.AddHours(1), end.AddHours(1));

        // Assert
        Assert.True(interval1 == interval2);
        Assert.False(interval1 == interval3);
        Assert.True(interval1 != interval3);
        Assert.True(interval1.Equals(interval2));
        Assert.False(interval1.Equals(interval3));
    }
}