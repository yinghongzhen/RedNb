using Volo.Abp.Application.Dtos;

namespace RedNb.WebGateway.Application.Contracts.Tests;

public class TestDto : EntityDto<long>
{
    public string Name { get; set; }
}