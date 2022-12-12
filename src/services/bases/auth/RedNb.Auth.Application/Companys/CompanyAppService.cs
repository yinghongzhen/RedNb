using RedNb.Auth.Application.Contracts;
using RedNb.Auth.Application.Contracts.Companys;
using RedNb.Auth.Application.Contracts.Companys.Dtos;
using RedNb.Core.Contracts;
using RedNb.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Linq;
using RedNb.Core.Util;
using RedNb.Auth.Domain.Offices;
using RedNb.Core.Domain;
using RedNb.Core.Application;

namespace RedNb.Auth.Application.Companys
{
    public class CompanyAppService : ICompanyAppService
    {
        private readonly IRepository<Company, long> _companyRepository;
        private readonly IObjectMapper _objectMapper;

        public CompanyAppService(IRepository<Company, long> CompanyRepository,
            IObjectMapper objectMapper)
        {
            _companyRepository = CompanyRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(CompanyAddInputDto input)
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

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var model = await _companyRepository.GetAsync(item);

                if (await _companyRepository.AnyAsync(m => m.ParentId == model.Id))
                {
                    throw new UserFriendlyException("节点下有子节点，不能删除");
                }

                await _companyRepository.DeleteAsync(model);
            }
        }

        public async Task UpdateAsync(CompanyUpdateInputDto input)
        {
            var model = await _companyRepository.GetAsync(input.Id);

            var queryable = await _companyRepository.GetQueryableAsync();

            _objectMapper.Map(input, model);

            var parentId = $"{model.Id},";

            var children = await queryable
                .Where(m => m.ParentIds.Contains(parentId))
                .OrderBy(m => m.TreeLevel)
                .ThenBy(m => m.Id)
                .Select(m => (TreeEntity)m)
                .ToListAsync();

            if (model.ParentId != 0)
            {
                var parent = await _companyRepository.GetAsync(model.ParentId);

                model.UpdateTreeValue(parent, children);
            }
            else
            {
                model.UpdateTreeValue(null, children);
            }

            if (await _companyRepository.AnyAsync(m => m.Key == input.Key &&
                m.Id != model.Id &&
                m.TreeLevel == model.TreeLevel &&
                m.ParentId == model.ParentId &&
                m.TenantId == model.TenantId))
            {
                throw new UserFriendlyException("编码已存在，添加失败");
            }
        }

        public async Task<List<AntdTreeOutputDto>> GetTreeAsync(CompanyGetTreeInputDto input)
        {
            var queryable = await _companyRepository.GetQueryableAsync();

            if (input.ExcludeIds != null)
            {
                queryable = queryable.Where(m => !input.ExcludeIds.Contains(m.Id));
            }

            var list = await queryable
                .OrderByDescending(m => m.TreeSort)
                .ThenBy(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Company>, List<CompanyOutputDto>>(list);

            var topData = data.Where(m => m.TreeLevel == 0).ToList();
            var leafData = data.Where(m => m.TreeLevel != 0).ToList();

            foreach (var item in topData)
            {
                CommonHelper.ReGetTree(item, leafData);
            }

            return _objectMapper.Map<List<CompanyOutputDto>, List<AntdTreeOutputDto>>(topData);
        }

        public async Task<PagedOutputDto<CompanyOutputDto>> GetPageAsync(CompanyGetPageInputDto input)
        {
            var queryable = await _companyRepository.GetQueryableAsync();

            var topQuery = queryable.Where(m => m.TreeLevel == 0);

            if (!String.IsNullOrWhiteSpace(input.TreeName))
            {
                topQuery = topQuery.Where(m => m.TreeName.Contains(input.TreeName));
            }

            var count = await topQuery.CountAsync();

            var topList = await topQuery
                .OrderBy(m => m.TreeLevel)
                .ThenByDescending(m => m.TreeSort)
                .PageBy(input)
                .ToListAsync();

            var leafList = await queryable
                .Where(m => m.TreeLevel > 0)
                .OrderBy(m => m.TreeLevel)
                .ThenByDescending(m => m.TreeSort)
                .ToListAsync();

            var topData = _objectMapper.Map<List<Company>, List<CompanyOutputDto>>(topList);
            var leafData = _objectMapper.Map<List<Company>, List<CompanyOutputDto>>(leafList);

            foreach (var item in topData)
            {
                CommonHelper.ReGetTree(item, leafData);
            }

            return new PagedOutputDto<CompanyOutputDto>(count, topData);
        }
    }
}
