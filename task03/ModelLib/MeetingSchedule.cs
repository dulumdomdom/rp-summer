using System.Diagnostics.CodeAnalysis;

namespace ModelLib;

public class MeetingSchedule
{
    private readonly Dictionary<string, DateTimeInterval> _meetings = new();
    private readonly List<DateTimeInterval> _sortedIntervals = new();

    public MeetingSchedule() { }

    public bool IsBusy(DateTimeInterval interval)
    {
        // Используем бинарный поиск для эффективности
        int index = _sortedIntervals.BinarySearch(interval);

        // Проверяем соседние интервалы на пересечение
        for (int i = Math.Max(0, ~index - 1); i < Math.Min(_sortedIntervals.Count, ~index + 2); i++)
        {
            if (_sortedIntervals[i].Overlaps(interval))
            {
                return true; // Занят, если есть пересечение
            }
        }

        return false; // Свободен, если нет пересечений
    }

    public void Add(string meetingId, DateTimeInterval interval)
    {
        if (string.IsNullOrEmpty(meetingId))
        {
            throw new ArgumentException("Meeting ID cannot be null or empty", nameof(meetingId));
        }

        if (_meetings.ContainsKey(meetingId))
        {
            return; // Встреча уже существует, ничего не делаем
        }

        if (interval.End <= DateTime.Now)
        {
            throw new InvalidOperationException("Cannot add a meeting that has already ended");
        }

        if (IsBusy(interval))
        {
            throw new InvalidOperationException("The interval overlaps with existing meetings");
        }

        _meetings.Add(meetingId, interval);

        // Добавляем в отсортированный список
        int index = _sortedIntervals.BinarySearch(interval);
        if (index < 0)
        {
            _sortedIntervals.Insert(~index, interval);
        }
        else
        {
            _sortedIntervals.Insert(index, interval);
        }
    }

    public bool ContainsMeeting(string meetingId)
    {
        return _meetings.ContainsKey(meetingId);
    }

    public DateTimeInterval? GetMeetingInterval(string meetingId)
    {
        // Явный тип вместо var
        if (_meetings.TryGetValue(meetingId, out DateTimeInterval interval))
        {
            return interval;
        }
        return null;
    }

    public IEnumerable<KeyValuePair<string, DateTimeInterval>> GetAllMeetings()
    {
        return _meetings.OrderBy(m => m.Value);
    }
}