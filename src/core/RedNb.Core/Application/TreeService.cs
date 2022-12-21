using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Volo.Abp.ObjectMapping;

namespace RedNb.Core.Domain;

public abstract class TreeService<T> where T : TreeEntity
{
    public async Task AddAsync(T input)
    {
        var model = _objectMapper.Map<CompanyAddInputDto, Company>(input);

        model.CreateKey();

        if (input.ParentId != 0)
        {
            var parent = await _companyRepository.GetAsync(input.ParentId);

            model.UpdateTreeValue(parent, null);
        }
        else
        {
            model.UpdateTreeValue(null, null);
        }

        if (await _companyRepository.AnyAsync(m => m.Key == input.Key &&
                    m.TreeLevel == model.TreeLevel &&
                    m.ParentId == model.ParentId &&
                    m.TenantId == model.TenantId))
        {
            throw new UserFriendlyException("编码已存在，添加失败");
        }

        await _companyRepository.InsertAsync(model);
    }
}
