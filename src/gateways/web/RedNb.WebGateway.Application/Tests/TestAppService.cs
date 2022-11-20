using RedNb.WebGateway.Application.Contracts.Tests;
using RedNb.WebGateway.Domain.Tests;
using Volo.Abp.ObjectMapping;
using Volo.Abp;
using RedNb.Core.Util;
using Microsoft.EntityFrameworkCore;

namespace RedNb.WebGateway.Application.Tests;

public class TestAppService : ITestAppService
{
    private readonly IRepository<Test, long> _testRepository;
    private readonly IObjectMapper _objectMapper;

    public TestAppService(
        IRepository<Test, long> testRepository,
        IObjectMapper objectMapper)
    {
        _testRepository = testRepository;
        _objectMapper = objectMapper;
    }

    public async Task AddAsync(TestAddInputDto input)
    {
        //await _testManager.CreateAsync(input.Name);
    }

    public async Task GetPageAsync()
    {
        var query = await _testRepository.GetQueryableAsync();

        var first = await query.FirstOrDefaultAsync();

        var second = _objectMapper.Map<Test, TestOutputDto>(first);   
    }
}
