using RedNb.Core.Contracts;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace RedNb.Core.Domain;

public abstract class TreeAppService<TEntity, TAddInputDto, TUpdateInputDto, TOutputDto> : IApplicationService, ITransientDependency
     where TEntity : TreeAggregateRoot, new()
     where TAddInputDto : TreeAddInputDto
     where TUpdateInputDto : TreeUpdateInputDto
     where TOutputDto : TreeOutputDto<TOutputDto>
{
    private readonly IRepository<TEntity, long> _entityRepository;
    private readonly IObjectMapper _objectMapper;

    public TreeAppService(IRepository<TEntity, long> entityRepository,
        IObjectMapper objectMapper)
    {
        _entityRepository = entityRepository;
        _objectMapper = objectMapper;
    }

    public async Task AddAsync(TAddInputDto input)
    {
        var model = _objectMapper.Map<TAddInputDto, TEntity>(input);

        if (model.ParentId != 0)
        {
            var parent = await _entityRepository.GetAsync(model.ParentId);

            if (parent == null)
            {
                throw new BusinessException("父节点不存在");
            }

            model.UpdateTreeValue(parent, null);
        }
        else
        {
            model.UpdateTreeValue(null, null);
        }

        await _entityRepository.InsertAsync(model);
    }

    public async Task UpdateAsync(TUpdateInputDto input)
    {
        var model = _objectMapper.Map<TUpdateInputDto, TEntity>(input);

        var old = await _entityRepository.GetAsync(model.Id);

        var queryable = await _entityRepository.GetQueryableAsync();

        var oldParentId = $"{old.ParentIds}{old.Id}";

        var oldChildren = await queryable
            .Where(m => m.ParentIds.Contains(oldParentId))
            .OrderBy(m => m.Level)
            .ThenBy(m => m.Id)
            .Select(m => (TreeAggregateRoot)m)
            .ToListAsync();

        old.Name = model.Name;
        old.ParentId = model.ParentId;
        old.Sort = model.Sort;

        if (old.ParentId != 0)
        {
            var parent = await _entityRepository.GetAsync(old.ParentId);

            old.UpdateTreeValue(parent, oldChildren);
        }
        else
        {
            old.UpdateTreeValue(null, oldChildren);
        }
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
