using RedNb.Core.Contracts;
using System.Diagnostics.Metrics;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RedNb.Core.Domain;

public abstract class TreeService<TEntity, TOutputDto> : DomainService where TEntity : TreeAggregateRoot, new()
{
    private readonly IRepository<TEntity, long> _treeEntityRepository;
    private readonly IObjectMapper _objectMapper;

    public TreeService(IRepository<TEntity, long> treeEntityRepository, IObjectMapper objectMapper)
    {
        _treeEntityRepository = treeEntityRepository;
        _objectMapper = objectMapper;
    }

    public virtual async Task AddAsync(TEntity model)
    {
        model.CreateKey();

        if (model.ParentId != 0)
        {
            var parent = await _treeEntityRepository.GetAsync(model.ParentId);

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

        await _treeEntityRepository.InsertAsync(model);
    }

    public virtual async Task UpdateAsync(TEntity model)
    {
        var old = await _treeEntityRepository.GetAsync(model.Id);

        var queryable = await _treeEntityRepository.GetQueryableAsync();

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
            var parent = await _treeEntityRepository.GetAsync(old.ParentId);

            old.UpdateTreeValue(parent, oldChildren);
        }
        else
        {
            old.UpdateTreeValue(null, oldChildren);
        }
    }

    public virtual async Task GetListAsync(TEntity model)
    {
        var old = await _treeEntityRepository.GetAsync(model.Id);

        var queryable = await _treeEntityRepository.GetQueryableAsync();

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
            var parent = await _treeEntityRepository.GetAsync(old.ParentId);

            old.UpdateTreeValue(parent, oldChildren);
        }
        else
        {
            old.UpdateTreeValue(null, oldChildren);
        }
    }

    public virtual async Task<PagedOutputDto<TOutputDto>> GetPageAsync(PagedInputDto input)
    {
        var queryable = await _treeEntityRepository.GetQueryableAsync();

        var count = await queryable.CountAsync();

        var list = await queryable
            .OrderBy(m => m.Sorts)
            .ThenBy(m => m.Id)
            .PageBy(input)
            .ToListAsync();

        var data = _objectMapper.Map<List<TEntity>, List<TOutputDto>>(list);

        return new PagedOutputDto<TOutputDto>(count, data);
    }
}
