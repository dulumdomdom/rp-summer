using System.Diagnostics.CodeAnalysis;

namespace ModelLib;

public readonly struct DateTimeInterval : IComparable<DateTimeInterval>
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public TimeSpan Duration => End - Start;

    public DateTimeInterval(DateTime start, DateTime end)
    {
        if (end <= start)
        {
            throw new ArgumentException("End must be greater than start", nameof(end));
        }

        Start = start;
        End = end;
    }

    public bool Overlaps(DateTimeInterval other)
    {
        // Интервалы не пересекаются, если один закончился до начала другого
        // Или начался после окончания другого
        return Start < other.End && other.Start < End;
    }

    public int CompareTo(DateTimeInterval other)
    {
        return Start.CompareTo(other.Start);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is DateTimeInterval other &&
               Start == other.Start &&
               End == other.End;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }

    public static bool operator ==(DateTimeInterval left, DateTimeInterval right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DateTimeInterval left, DateTimeInterval right)
    {
        return !(left == right);
    }

    public static bool operator <(DateTimeInterval left, DateTimeInterval right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(DateTimeInterval left, DateTimeInterval right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(DateTimeInterval left, DateTimeInterval right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(DateTimeInterval left, DateTimeInterval right)
    {
        return left.CompareTo(right) >= 0;
    }

    public override string ToString()
    {
        return $"{Start:yyyy-MM-dd HH:mm} - {End:yyyy-MM-dd HH:mm}";
    }
}