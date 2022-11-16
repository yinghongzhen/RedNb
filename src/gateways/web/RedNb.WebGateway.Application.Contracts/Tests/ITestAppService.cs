namespace RedNb.WebGateway.Application.Contracts.Tests;

public interface ITestAppService : IApplicationService, ITransientDependency
{
    public Task AddAsync(TestAddInputDto input);
}
