namespace DKZKV.BookStore.Domain.SeedWork;

public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(T? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        using IEnumerator<object> thisValues = GetEqualityComponents().GetEnumerator();
        using IEnumerator<object> otherValues = other.GetEqualityComponents().GetEnumerator();

        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (thisValues.Current is null ^ otherValues.Current is null)
            {
                return false;
            }

            if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is T other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hashCode = 0;

        foreach (var value in GetEqualityComponents())
        {
            hashCode = HashCode.Combine(hashCode, value);
        }

        return hashCode;
    }

    public static bool operator == (ValueObject<T>? left, ValueObject<T>? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator != (ValueObject<T> left, ValueObject<T> right)
    {
        return !(left == right);
    }
}