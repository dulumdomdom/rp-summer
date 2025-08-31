namespace ModelLib.Tests;

public class MeetingRoomTests
{
    private DateTime GetFutureDateTime(int hoursFromNow = 1)
    {
        return DateTime.Now.AddHours(hoursFromNow);
    }

    [Fact]
    public void Constructor_WithValidName_ShouldCreateRoom()
    {
        // Act
        MeetingRoom room = new MeetingRoom("Conference Room A");

        // Assert
        Assert.Equal("Conference Room A", room.Name);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new MeetingRoom(""));
    }

    [Fact]
    public void AddMeeting_ShouldDelegateToSchedule()
    {
        // Arrange
        MeetingRoom room = new MeetingRoom("Test Room");
        DateTimeInterval interval = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(2));

        // Act
        room.AddMeeting("meeting1", interval);

        // Assert
        Assert.True(room.ContainsMeeting("meeting1"));
        Assert.True(room.IsBusy(interval)); // Должно быть true, так как интервал занят
    }

    [Fact]
    public void IsBusy_ShouldDelegateToSchedule()
    {
        // Arrange
        MeetingRoom room = new MeetingRoom("Test Room");
        DateTimeInterval interval1 = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(2));
        DateTimeInterval interval2 = new DateTimeInterval(
            GetFutureDateTime(2), // Используем целые числа вместо 1.5
            GetFutureDateTime(3));

        room.AddMeeting("meeting1", interval1);

        // Act
        bool isBusy = room.IsBusy(interval2);

        // Assert
        // Интервалы не пересекаются (касаются вплотную), поэтому должно быть false
        Assert.False(isBusy);
    }

    [Fact]
    public void IsBusy_WithOverlappingIntervals_ShouldReturnTrue()
    {
        // Arrange
        MeetingRoom room = new MeetingRoom("Test Room");
        DateTimeInterval interval1 = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(3));
        DateTimeInterval interval2 = new DateTimeInterval(
            GetFutureDateTime(2),
            GetFutureDateTime(4));

        room.AddMeeting("meeting1", interval1);

        // Act
        bool isBusy = room.IsBusy(interval2);

        // Assert
        Assert.True(isBusy);
    }

    [Fact]
    public void GetAllMeetings_ShouldReturnMeetingsFromSchedule()
    {
        // Arrange
        MeetingRoom room = new MeetingRoom("Test Room");
        DateTimeInterval interval = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(2));

        room.AddMeeting("meeting1", interval);

        // Act
        List<KeyValuePair<string, DateTimeInterval>> meetings = room.GetAllMeetings().ToList();

        // Assert
        Assert.Single(meetings);
        Assert.Equal("meeting1", meetings[0].Key);
    }

    [Fact]
    public void Equality_ShouldWorkCorrectly()
    {
        // Arrange
        MeetingRoom room1 = new MeetingRoom("Room A");
        MeetingRoom room2 = new MeetingRoom("Room A");
        MeetingRoom room3 = new MeetingRoom("Room B");

        // Assert
        Assert.True(room1.Equals(room2));
        Assert.False(room1.Equals(room3));
        Assert.True(room1 == room2);
        Assert.False(room1 == room3);
    }
}