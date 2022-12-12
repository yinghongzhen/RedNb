using RedNb.Auth.Application.Contracts.DictTypes;
using RedNb.Auth.Application.Contracts.DictTypes.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using RestSharp;
using RedNb.Auth.Application.Contracts.DictDatas.Dtos;

namespace RedNb.Auth.Application.DictTypes
{
    public class DictTypeAppService : IDictTypeAppService
    {
        private readonly IRepository<DictType, long> _dictTypeRepository;
        private readonly IRepository<DictData, long> _dictDataRepository;
        private readonly IObjectMapper _objectMapper;

        public DictTypeAppService(IRepository<DictType, long> dictTypeRepository,
            IRepository<DictData, long> dictDataRepository,
            IObjectMapper objectMapper)
        {
            _dictTypeRepository = dictTypeRepository;
            _dictDataRepository = dictDataRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(DictTypeAddInputDto input)
        {
            if (await _dictTypeRepository.AnyAsync(m =>
                m.Type == input.Type))
            {
                throw new UserFriendlyException("类型已存在");
            }

            var model = _objectMapper.Map<DictTypeAddInputDto, DictType>(input);
            model.CreateKey();

            if (input.DictDataList != null)
            {
                foreach (var item in input.DictDataList)
                {
                    var dictData = _objectMapper.Map<DictDataAddInputDto, DictData>(item);

                    dictData.CreateKey();
                    dictData.DictTypeId = model.Id;

                    await _dictDataRepository.InsertAsync(dictData);
                }
            }

            await _dictTypeRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var dictType = await _dictTypeRepository.GetAsync(item);

                var dictDatas = await _dictDataRepository
                    .GetListAsync(m => m.DictTypeId == dictType.Id);

                foreach (var dictData in dictDatas)
                {
                    await _dictDataRepository.DeleteAsync(dictData);
                }

                await _dictTypeRepository.DeleteAsync(dictType);
            }
        }

        public async Task UpdateAsync(DictTypeUpdateInputDto input)
        {
            if (await _dictTypeRepository.AnyAsync(m =>
                m.Type == input.Type &&
                m.Id != input.Id))
            {
                throw new UserFriendlyException("类型已存在");
            }

            var model = await _dictTypeRepository.GetAsync(input.Id);

            _objectMapper.Map(input, model);

            if (input.DictDataList.Any())
            {
                var ids = input.DictDataList.Where(m => m.Id != 0).Select(m => m.Id).ToList();

                var queryable = await _dictDataRepository.GetQueryableAsync();

                var deleteList = await queryable
                    .Where(m => m.DictTypeId == model.Id &&
                    !ids.Contains(m.Id))
                    .ToListAsync();

                foreach (var item in deleteList)
                {
                    await _dictDataRepository.DeleteAsync(item);
                }

                foreach (var item in input.DictDataList)
                {
                    if (item.Id == 0)
                    {
                        var dictData = _objectMapper.Map<DictDataUpdateInputDto, DictData>(item);

                        dictData.CreateKey();
                        dictData.DictTypeId = model.Id;

                        await _dictDataRepository.InsertAsync(dictData);
                    }
                    else
                    {
                        var dictData = await _dictDataRepository
                            .SingleOrDefaultAsync(m => m.Id == item.Id);

                        _objectMapper.Map(item, dictData);
                    }
                }
            }
        }

        public async Task<PagedOutputDto<DictTypeOutputDto>> GetPageAsync(DictTypeGetPageInputDto input)
        {
            var queryable = await _dictTypeRepository.GetQueryableAsync();

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<DictType>, List<DictTypeOutputDto>>(list);

            var dictTypeIds = data.Select(m => m.Id).ToList();

            var dictDataQueryable = await _dictDataRepository.GetQueryableAsync();

            var dictDataList = await dictDataQueryable
                .Where(m => dictTypeIds.Contains(m.DictTypeId))
                .OrderBy(m => m.Id)
                .ToListAsync();

            foreach (var item in data)
            {
                var tDictDatas = dictDataList.Where(m => m.DictTypeId == item.Id).ToList();

                item.DictDataList = _objectMapper.Map<List<DictData>, List<DictDataOutputDto>>(tDictDatas);
            }

            return new PagedOutputDto<DictTypeOutputDto>(count, data);
        }
    }
}