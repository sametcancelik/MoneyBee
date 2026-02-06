namespace MoneyBee.Shared.Entities;
public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
