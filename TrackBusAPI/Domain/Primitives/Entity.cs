namespace PratiBus.Primitives;

public abstract class Entity
{
    public int Id { get; private set; }  // Auto-increment ID (int tip)

    protected Entity() { }

    // Implementacija Equals za poređenje entiteta prema ID-u
    public override bool Equals(object obj)
    {
        if (obj is Entity otherEntity)
        {
            return Id.Equals(otherEntity.Id);
        }
        return false;
    }

    // HashCode zasnovan na ID-u
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
