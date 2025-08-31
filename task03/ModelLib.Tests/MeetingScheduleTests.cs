namespace ModelLib.Tests;

public class MeetingScheduleTests
{
    private DateTime GetFutureDateTime(int hoursFromNow = 1)
    {
        return DateTime.Now.AddHours(hoursFromNow);
    }

    [Fact]
    public void IsBusy_WithEmptySchedule_ShouldReturnFalse()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTimeInterval interval = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(2));

        // Act
        bool result = schedule.IsBusy(interval);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Add_WithNonOverlappingInterval_ShouldAddMeeting()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTimeInterval interval = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(2));

        // Act
        schedule.Add("meeting1", interval);

        // Assert
        Assert.True(schedule.ContainsMeeting("meeting1"));
        Assert.True(schedule.IsBusy(interval)); // Должно быть true, так как интервал занят
    }

    [Fact]
    public void IsBusy_WithNonOverlappingInterval_ShouldReturnFalse()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTimeInterval interval1 = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(2));
        DateTimeInterval interval2 = new DateTimeInterval(
            GetFutureDateTime(3),
            GetFutureDateTime(4));

        schedule.Add("meeting1", interval1);

        // Act
        bool result = schedule.IsBusy(interval2);

        // Assert
        Assert.False(result); // Должно быть false, так как интервал свободен
    }

    [Fact]
    public void Add_WithOverlappingInterval_ShouldThrowInvalidOperationException()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTimeInterval interval1 = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(3));
        DateTimeInterval interval2 = new DateTimeInterval(
            GetFutureDateTime(2),
            GetFutureDateTime(4));

        schedule.Add("meeting1", interval1);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => schedule.Add("meeting2", interval2));
    }

    [Fact]
    public void Add_WithAdjacentInterval_ShouldAddMeeting()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTime start = GetFutureDateTime(1);
        DateTimeInterval interval1 = new DateTimeInterval(
            start,
            start.AddHours(1));
        DateTimeInterval interval2 = new DateTimeInterval(
            start.AddHours(1),
            start.AddHours(2));

        // Act
        schedule.Add("meeting1", interval1);
        schedule.Add("meeting2", interval2);

        // Assert
        Assert.True(schedule.ContainsMeeting("meeting1"));
        Assert.True(schedule.ContainsMeeting("meeting2"));
    }

    [Fact]
    public void Add_WithPastMeeting_ShouldThrowInvalidOperationException()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTimeInterval pastInterval = new DateTimeInterval(
            DateTime.Now.AddHours(-2),
            DateTime.Now.AddHours(-1));

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => schedule.Add("meeting1", pastInterval));
    }

    [Fact]
    public void Add_WithExistingMeetingId_ShouldDoNothing()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTimeInterval interval = new DateTimeInterval(
            GetFutureDateTime(1),
            GetFutureDateTime(2));

        schedule.Add("meeting1", interval);

        // Act
        schedule.Add("meeting1", interval); // Повторное добавление

        // Assert
        Assert.True(schedule.ContainsMeeting("meeting1"));
    }

    [Fact]
    public void GetAllMeetings_ShouldReturnMeetingsInOrder()
    {
        // Arrange
        MeetingSchedule schedule = new MeetingSchedule();
        DateTime start = GetFutureDateTime(1);
        DateTimeInterval interval1 = new DateTimeInterval(
            start.AddHours(2),
            start.AddHours(3));
        DateTimeInterval interval2 = new DateTimeInterval(
            start,
            start.AddHours(1));

        schedule.Add("meeting2", interval2);
        schedule.Add("meeting1", interval1);

        // Act
        List<KeyValuePair<string, DateTimeInterval>> meetings = schedule.GetAllMeetings().ToList();

        // Assert
        Assert.Equal(2, meetings.Count);
        Assert.Equal("meeting2", meetings[0].Key); // Должны быть отсортированы по времени
        Assert.Equal("meeting1", meetings[1].Key);
    }
}