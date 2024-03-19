namespace WeatherDDD.SharedKernel;

public abstract class BaseEntity<TId>
{
    public required TId Id { get; set; }

    public readonly List<BaseDomainEvent> Events = [];
    
}
