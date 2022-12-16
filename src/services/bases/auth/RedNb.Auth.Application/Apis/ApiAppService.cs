using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RedNb.Auth.Application.Contracts.Apis;
using RedNb.Auth.Application.Contracts.Apis.Dtos;
using RedNb.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp;
using RedNb.Core.Util;
using RestSharp;
using System.Text.Json;
using RedNb.Core.Domain;
using RedNb.Auth.Domain.Services;

namespace RedNb.Auth.Application.Apis
{
    public class ApiAppService : IApiAppService
    {
        private readonly IRepository<Module, long> _moduleRepository;
        private readonly IRepository<Instance, long> _instanceRepository;
        private readonly IRepository<Api, long> _apiRepository;
        private readonly IObjectMapper _objectMapper;

        public ApiAppService(IRepository<Module, long> moduleRepository,
            IRepository<Instance, long> instanceRepository,
            IRepository<Api, long> apiRepository,
            IObjectMapper objectMapper)
        {
            _moduleRepository = moduleRepository;
            _instanceRepository = instanceRepository;
            _apiRepository = apiRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(ApiAddInputDto input)
        {
            if (await _apiRepository.AnyAsync(m =>
                m.Path == input.Path))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = _objectMapper.Map<ApiAddInputDto, Api>(input);

            model.CreateKey();

            await _apiRepository.InsertAsync(model);
        }

        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            foreach (var item in input.Ids)
            {
                var config = await _apiRepository.GetAsync(item);

                await _apiRepository.DeleteAsync(config);
            }
        }

        public async Task UpdateAsync(ApiUpdateInputDto input)
        {
            if (await _apiRepository.AnyAsync(m =>
                m.Path == input.Path &&
                m.Id != input.Id))
            {
                throw new UserFriendlyException("编码已存在");
            }

            var model = await _apiRepository.GetAsync(input.Id);

            _objectMapper.Map(input, model);
        }

        public async Task<PagedOutputDto<ApiOutputDto>> GetPageAsync(ApiGetPageInputDto input)
        {
            var queryable = await _apiRepository.GetQueryableAsync();

            queryable = queryable.Where(m => m.ModuleId == input.ModuleId);

            var count = await queryable.CountAsync();

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .PageBy(input)
                .ToListAsync();

            var data = _objectMapper.Map<List<Api>, List<ApiOutputDto>>(list);

            return new PagedOutputDto<ApiOutputDto>(count, data);
        }

        public async Task SyncAsync(BatchInputDto input)
        {
            foreach (var id in input.Ids)
            {
                var module = await _moduleRepository.GetAsync(id);

                var instance = await _instanceRepository.FirstOrDefaultAsync(m => m.ModuleId == id);

                var apis = await _apiRepository
                            .GetListAsync(m => m.ModuleId == module.Id);

                var _client = new RestClient($"http://{instance.Host}:{instance.Port}");

                var request = new RestRequest($"swagger/{module.Version}/swagger.json", Method.Get);

                try
                {
                    var result = await _client.GetAsync<JsonElement>(request);

                    var pathNode = result.GetProperty("paths");

                    foreach (var node in pathNode.EnumerateObject())
                    {
                        foreach (var item in node.Value.EnumerateObject())
                        {
                            var method = EHttpMethod.Get;

                            switch (item.Name)
                            {
                                case "get":
                                    method = EHttpMethod.Get;
                                    break;
                                case "post":
                                    method = EHttpMethod.Post;
                                    break;
                                case "put":
                                    method = EHttpMethod.Put;
                                    break;
                                case "delete":
                                    method = EHttpMethod.Delete;
                                    break;
                                default:
                                    break;
                            }

                            var path = node.Name.Replace("/api/app/", $"/{module.Key}/");

                            var name = path;

                            JsonElement summaryJE;

                            if (item.Value.TryGetProperty("summary", out summaryJE))
                            {
                                name = summaryJE.GetString();
                            }

                            var tagNode = item.Value.GetProperty("tags");
                            var tagList = new List<string>();

                            foreach (var tag in tagNode.EnumerateArray())
                            {
                                tagList.Add(tag.GetString());
                            }

                            var api = apis.SingleOrDefault(m => m.Path == path &&
                                m.Method == method &&
                                m.ModuleId == module.Id);

                            var tags = String.Join(",", tagList);

                            if (api == null)
                            {
                                api = new Api()
                                {
                                    Name = name,
                                    Path = path,
                                    Method = method,
                                    IsActive = true,
                                    Tags = tags,
                                    ModuleId = module.Id
                                };

                                api.CreateKey();

                                await _apiRepository.InsertAsync(api);
                            }
                            else
                            {
                                api.Name = name;
                                api.Tags = tags;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new UserFriendlyException("swagger文档不存在");
                }
            }
        }
    }
}

