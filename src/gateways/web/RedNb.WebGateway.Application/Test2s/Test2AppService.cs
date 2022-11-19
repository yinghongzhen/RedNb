using RedNb.WebGateway.Application.Contracts.Test2s;
using RedNb.WebGateway.Domain.Test2s;
using Volo.Abp.ObjectMapping;
using Volo.Abp;
using RedNb.Core.Util;

namespace RedNb.WebGateway.Application.Tests;

public class Test2AppService : ITest2AppService
{
    private readonly Test2Manager _test2Manager;
    private readonly IRepository<Test2, Guid> _test2Repository;
    private readonly IObjectMapper _objectMapper;

    public Test2AppService(
        Test2Manager test2Manager,
        IRepository<Test2, Guid> test2Repository,
        IObjectMapper objectMapper)
    {
        _test2Manager = test2Manager;
        _test2Repository = test2Repository;
        _objectMapper = objectMapper;
    }

    public async Task AddAsync(Test2AddInputDto input)
    {
        await _test2Manager.CreateAsync(input.Name);
    }

    public async Task<List<Test2OutputDto>> GetPageAsync()
    {
        var list = await _test2Repository.GetListAsync();

        return _objectMapper.Map<List<Test2>, List<Test2OutputDto>>(list);
    }
}
