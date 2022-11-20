using RedNb.Core.Contracts;
using Volo.Abp.Application.Dtos;

namespace RedNb.WebGateway.Application.Contracts.Tests;

public class TestOutputDto : EntityBaseDto
{
    public string Name { get; set; }
}