using Volo.Abp.Application.Dtos;

namespace RedNb.WebGateway.Application.Contracts.Tests;

public class TestOutputDto : EntityDto<long>
{
    public string Name { get; set; }
}