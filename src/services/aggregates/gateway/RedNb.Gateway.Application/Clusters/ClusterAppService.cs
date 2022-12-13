using Microsoft.Extensions.Localization;
using RedNb.Gateway.Application.Contracts.Clusters;
using RedNb.Gateway.Domain.Clusters;
using RedNb.Gateway.Domain.Shared.Localization;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Localization;
using Volo.Abp.ObjectMapping;

namespace RedNb.Gateway.Application.Clusters;

public class ClusterAppService : IClusterAppService
{
    private readonly ClusterManager _clusterManager;
    private readonly IRepository<Cluster, long> _clusterRepository;
    private readonly IStringLocalizer<GatewayResource> _stringLocalizer;
    private readonly IObjectMapper _objectMapper;

    public ClusterAppService(
        IRepository<Cluster, long> ClusterRepository,
        IObjectMapper objectMapper,
        IStringLocalizer<GatewayResource> stringLocalizer,
        ClusterManager clusterManager)
    {
        _clusterRepository = ClusterRepository;
        _objectMapper = objectMapper;
        _clusterManager = clusterManager;
        _stringLocalizer = stringLocalizer;

    }

    public async Task AddAsync(ClusterAddInputDto input)
    {
        throw new BusinessException("Gateway:00001");

        await _clusterManager.CreateAsync(input.Name);
    }

    public async Task UpdateAsync(ClusterUpdateInputDto input)
    {
        var cluster = await _clusterRepository.GetAsync(input.Id);
    }

    public async Task<List<ClusterOutputDto>> GetListAsync()
    {
        var list = await _clusterRepository.GetListAsync();

        return _objectMapper.Map<List<Cluster>, List<ClusterOutputDto>>(list);
    }
}
