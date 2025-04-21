namespace PratiBus.Primitives;

public abstract class ValueObject
{
    // Proverava da li su dva Value Object-a ista
    public override bool Equals(object obj)
    {
        if (obj is ValueObject other)
        {
            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
        return false;
    }

    // Metoda koja vraća HashCode za Value Object
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
    }

    // Apstraktna metoda koja definiše logiku za poređenje komponenti Value Object-a
    protected abstract IEnumerable<object> GetEqualityComponents();
}
