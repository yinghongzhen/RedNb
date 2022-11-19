namespace RedNb.WebGateway.Application.Contracts.Test2s;

public interface ITest2AppService : IApplicationService, ITransientDependency
{
    public Task AddAsync(Test2AddInputDto input);
}
