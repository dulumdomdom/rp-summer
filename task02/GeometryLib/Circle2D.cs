using System.Diagnostics.CodeAnalysis;

namespace GeometryLib;

public class Circle2D : IEquatable<Circle2D>
{
    public const double Tolerance = 1e-10;
    public const int Precision = 10;

    private readonly double _radius;

    public Point2D Center { get; }

    public double Radius
    {
        get => _radius;
        init
        {
            if (value <= Tolerance)
            {
                throw new ArgumentException("Radius must be positive", nameof(value));
            }
            _radius = value;
        }
    }

    public double Diameter => 2 * Radius;
    public double Circumference => 2 * Math.PI * Radius;
    public double Area => Math.PI * Radius * Radius;

    public Circle2D(Point2D center, double radius)
    {
        if (radius <= Tolerance)
        {
            throw new ArgumentException("Radius must be positive", nameof(radius));
        }

        Center = center;
        _radius = radius;
    }

    /// <summary>
    /// Расстояние от точки до ближайшей точки окружности
    /// </summary>
    public double DistanceToPoint(Point2D point)
    {
        double distanceToCenter = Center.DistanceTo(point);
        return Math.Abs(distanceToCenter - Radius);
    }

    /// <summary>
    /// Расстояние между ближайшими точками двух окружностей
    /// </summary>
    public double DistanceToCircle(Circle2D other)
    {
        double centerDistance = Center.DistanceTo(other.Center);
        double distance = centerDistance - Radius - other.Radius;
        return Math.Max(0, distance);
    }

    /// <summary>
    /// Проверяет, лежит ли точка внутри круга
    /// </summary>
    public bool ContainsPoint(Point2D point)
    {
        double distanceToCenter = Center.DistanceTo(point);
        return distanceToCenter <= Radius + Tolerance;
    }

    /// <summary>
    /// Проверяет, лежит ли другой круг полностью внутри этого круга
    /// </summary>
    public bool ContainsCircle(Circle2D other)
    {
        double centerDistance = Center.DistanceTo(other.Center);
        // Проверяем, что расстояние между центрами + радиус другого круга <= радиус этого круга
        return centerDistance + other.Radius <= Radius + Tolerance;
    }

    /// <summary>
    /// Проверяет, пересекаются ли два круга
    /// </summary>
    public bool IntersectsWith(Circle2D other)
    {
        double centerDistance = Center.DistanceTo(other.Center);
        double sumRadii = Radius + other.Radius;
        double diffRadii = Math.Abs(Radius - other.Radius);

        return centerDistance <= sumRadii + Tolerance
               && centerDistance >= diffRadii - Tolerance;
    }

    public bool Equals(Circle2D? other)
    {
        if (other is null) return false;
        return Center.Equals(other.Center)
               && Math.Abs(Radius - other.Radius) < Tolerance;
    }

    public override bool Equals(object? obj)
    {
        return obj is Circle2D other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Math.Round(Center.X, Precision),
            Math.Round(Center.Y, Precision),
            Math.Round(Radius, Precision));
    }

    public override string ToString()
    {
        return $"Circle(Center: {Center}, Radius: {Radius})";
    }
}