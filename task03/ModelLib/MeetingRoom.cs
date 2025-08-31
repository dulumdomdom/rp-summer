using System.Diagnostics.CodeAnalysis;

namespace ModelLib;

public class MeetingRoom
{
    private readonly MeetingSchedule _schedule = new();

    public MeetingRoom(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Room name cannot be null or empty", nameof(name));
        }

        Name = name;
    }

    public static bool operator ==(MeetingRoom? left, MeetingRoom? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Name == right.Name;
    }

    public static bool operator !=(MeetingRoom? left, MeetingRoom? right)
    {
        return !(left == right);
    }

    public string Name { get; }

    public bool IsBusy(DateTimeInterval interval)
    {
        return _schedule.IsBusy(interval);
    }

    public void AddMeeting(string meetingId, DateTimeInterval interval)
    {
        _schedule.Add(meetingId, interval);
    }

    public bool ContainsMeeting(string meetingId)
    {
        return _schedule.ContainsMeeting(meetingId);
    }

    public DateTimeInterval? GetMeetingInterval(string meetingId)
    {
        return _schedule.GetMeetingInterval(meetingId);
    }

    public IEnumerable<KeyValuePair<string, DateTimeInterval>> GetAllMeetings()
    {
        return _schedule.GetAllMeetings();
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is MeetingRoom other && Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override string ToString()
    {
        return $"MeetingRoom: {Name}";
    }
}