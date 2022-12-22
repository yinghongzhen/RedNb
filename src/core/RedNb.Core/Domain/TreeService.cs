using System.Diagnostics.Metrics;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;

namespace RedNb.Core.Domain;

public abstract class TreeService<T> : DomainService where T : TreeAggregateRoot, new()
{
    private readonly IRepository<T, long> _treeEntityRepository;

    public TreeService(IRepository<T, long> treeEntityRepository)
    {
        _treeEntityRepository = treeEntityRepository;
    }

    public virtual async Task AddAsync(T model)
    {
        model.CreateKey();
        model.IsLast = false;

        if (model.ParentId != 0)
        {
            var parent = await _treeEntityRepository.GetAsync(model.ParentId);

            model.UpdateNodeValue(parent);
        }
        else
        {
            model.UpdateNodeValue(null);
        }

        await _treeEntityRepository.InsertAsync(model);
    }

    public virtual async Task UpdateAsync(T model)
    {
        var old = await _treeEntityRepository.GetAsync(model.Id);

        var queryable = await _treeEntityRepository.GetQueryableAsync();

        var oldParentId = $"{old.Id},";

        var oldChildren = await queryable
            .Where(m => m.ParentIds.Contains(oldParentId))
            .OrderBy(m => m.Level)
            .ThenBy(m => m.Id)
            .Select(m => (TreeAggregateRoot)m)
            .ToListAsync();

        if (old.ParentId == model.ParentId)
        {
            old.UpdateTreeValue(null, oldChildren);
        }
        else
        {
            var parent = await _treeEntityRepository.GetAsync(model.ParentId);

            model.UpdateTreeValue(parent, oldChildren);
        }
    }
}
