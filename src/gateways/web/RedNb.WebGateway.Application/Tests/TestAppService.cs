using RedNb.WebGateway.Application.Contracts.Tests;
using RedNb.WebGateway.Domain.Tests;
using Volo.Abp.ObjectMapping;
using Volo.Abp;
using RedNb.Core.Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RedNb.WebGateway.Application.Tests;

public class TestAppService : ITestAppService
{
    private readonly TestManager _testManager;
    private readonly IRepository<Test, long> _testRepository;
    private readonly IObjectMapper _objectMapper;

    public TestAppService(
        TestManager testManager,
        IRepository<Test, long> testRepository,
        IObjectMapper objectMapper)
    {
        _testManager = testManager;
        _testRepository = testRepository;
        _objectMapper = objectMapper;
    }

    public async Task AddAsync(TestAddInputDto input)
    {
        await _testManager.CreateAsync(input.Name);
    }

    public async Task<List<TestOutputDto>> GetPageAsync()
    {
        var list = await _testRepository.GetListAsync();

        return _objectMapper.Map<List<Test>, List<TestOutputDto>>(list);
    }
}
