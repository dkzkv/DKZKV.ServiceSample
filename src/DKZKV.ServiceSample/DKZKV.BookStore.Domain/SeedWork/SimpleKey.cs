namespace DKZKV.BookStore.Domain.SeedWork;

public abstract class SimpleKey<TKey> :
    ValueObject<SimpleKey<TKey>>,
    IComparable, IComparable<TKey>
    where TKey : SimpleKey<TKey>
{
    public Guid Value { get; private set; }

    protected SimpleKey(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Guid is empty", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return $"{typeof(TKey).Name} (Id = {Value})";
    }

    public int CompareTo(object obj)
    {
        var otherKey = obj as TKey;
        return CompareTo(otherKey);
    }

    public int CompareTo(TKey other)
    {
        return Comparer<Guid?>.Default.Compare(Value, other?.Value);
    }
}