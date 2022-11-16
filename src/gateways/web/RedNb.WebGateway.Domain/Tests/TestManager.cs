namespace RedNb.WebGateway.Domain.Tests;

public class TestManager : DomainService
{
    private readonly IRepository<Test, long> _testRepository;

    public TestManager(IRepository<Test, long> testRepository)
    {
        _testRepository = testRepository;
    }

    public async Task<Test> CreateAsync(string name)
    {
        return await _testRepository.InsertAsync(
            new Test(
                IdentityManager.NewId(),
                name
            )
        );
    }

}