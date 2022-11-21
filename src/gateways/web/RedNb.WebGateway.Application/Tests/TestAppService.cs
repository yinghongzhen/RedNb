using RedNb.WebGateway.Application.Contracts.Tests;
using RedNb.WebGateway.Domain.Tests;
using Volo.Abp.ObjectMapping;
using Volo.Abp;
using RedNb.Core.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        var model = _objectMapper.Map<TestAddInputDto, Test>(input);

        await _testRepository.InsertAsync(model);
    }

    public async Task<List<TestOutputDto>> GetPageAsync()
    {
        var list = await _testRepository.GetListAsync();

        var data = _objectMapper.Map<List<Test>, List<TestOutputDto>>(list);

        return data;
    }
}
