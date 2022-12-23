using RedNb.Core.Contracts;
using System.Diagnostics.Metrics;
using System.Security;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RedNb.Core.Domain;

public abstract class TreeAppService<TEntity, TAddInputDto, TUpdateInputDto, TOutputDto> : IApplicationService, ITransientDependency
     where TEntity : TreeAggregateRoot, new()
     where TAddInputDto : TreeAddInputDto
     where TUpdateInputDto : TreeUpdateInputDto
     where TOutputDto : TreeOutputDto<TOutputDto>
{
    private readonly TreeService<TEntity> _treeService;
    private readonly IObjectMapper _objectMapper;
    private readonly IRepository<TEntity, long> _entityRepository;

    public TreeAppService(TreeService<TEntity> treeService, IObjectMapper objectMapper, IRepository<TEntity, long> entityRepository)
    {
        _treeService = treeService;
        _objectMapper = objectMapper;
        _entityRepository = entityRepository;
    }

    public async Task AddAsync(TAddInputDto input)
    {
        var model = _objectMapper.Map<TAddInputDto, TEntity>(input);

        await _treeService.AddAsync(model);
    }

    public async Task UpdateAsync(TUpdateInputDto input)
    {
        var model = _objectMapper.Map<TUpdateInputDto, TEntity>(input);

        await _treeService.UpdateAsync(model);
    }

    public virtual async Task<List<TOutputDto>> GetListAsync()
    {
        var result = await GetPageAsync(new PagedInputDto()
        {
            PageIndex = 1,
            PageSize = int.MaxValue
        });

        return result.Items;
    }

    public virtual async Task<PagedOutputDto<TOutputDto>> GetPageAsync(PagedInputDto input)
    {
        var queryable = await _entityRepository.GetQueryableAsync();

        var topQuery = queryable.Where(m => m.Level == 0);

        var count = await topQuery.CountAsync();

        var topList = await topQuery
            .OrderBy(m => m.Sort)
            .ThenBy(m => m.Id)
            .PageBy(input)
            .ToListAsync();

        var leafList = await queryable
            .Where(m => m.Level > 0)
            .OrderBy(m => m.Sorts)
            .ThenBy(m => m.Id)
            .ToListAsync();

        var topData = _objectMapper.Map<List<TEntity>, List<TOutputDto>>(topList);
        var leafData = _objectMapper.Map<List<TEntity>, List<TOutputDto>>(leafList);

        foreach (var item in topData)
        {
            ReGetTree(item, leafData);
        }

        return new PagedOutputDto<TOutputDto>(count, topData);
    }

    private void ReGetTree(TOutputDto source, List<TOutputDto> data)
    {
        var nextData = data.Where(m => m.ParentId == source.Id).ToList();

        foreach (var item in nextData)
        {
            if (source.Children == null)
            {
                source.Children = new List<TOutputDto>();
            }

            source.Children.Add(item);

            ReGetTree(item, data);
        }
    }
}
