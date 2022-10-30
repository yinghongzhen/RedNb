namespace RedNb.Core.Domain;

public interface IHasTenant
{
    public long TenantId { get; set; }
}
