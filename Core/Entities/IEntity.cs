namespace Core.Entities
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
