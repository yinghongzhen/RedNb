using Volo.Abp.Application.Dtos;

namespace RedNb.WebGateway.Application.Contracts.Test2s;

public class Test2OutputDto : EntityDto<Guid>
{
    public string Name { get; set; }
}