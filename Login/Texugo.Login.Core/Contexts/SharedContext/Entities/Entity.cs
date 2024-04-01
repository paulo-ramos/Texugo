namespace Texugo.Login.Core.Contexts.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public bool Equals(Guid otherId) => Id == otherId;
    public override int GetHashCode() => Id.GetHashCode();
    
}