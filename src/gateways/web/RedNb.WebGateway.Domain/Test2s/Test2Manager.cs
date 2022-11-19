using Volo.Abp.Application.Dtos;

namespace RedNb.WebGateway.Domain.Test2s;

public class Test2Manager : DomainService
{
    private readonly IRepository<Test2, Guid> _test2Repository;

    public Test2Manager(IRepository<Test2, Guid> test2Repository)
    {
        _test2Repository = test2Repository;
    }

    public async Task<Test2> CreateAsync(string name)
    {
        return await _test2Repository.InsertAsync(
            new Test2(Guid.NewGuid(), name) 
        );
    }
}