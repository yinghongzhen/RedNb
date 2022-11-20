using RedNb.WebGateway.Application.Contracts.Tests;
using RedNb.WebGateway.Domain.Tests;

namespace RedNb.WebGateway.Application.Tests;

public class TestMapProfile : Profile
{
    public TestMapProfile()
    {
        CreateMap<TestAddInputDto, Test>();

        CreateMap<Test, TestOutputDto>();
    }
}