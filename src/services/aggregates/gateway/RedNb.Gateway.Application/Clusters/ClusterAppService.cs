﻿using RedNb.Core.Domain;
using RedNb.Gateway.Application.Contracts.Clusters;
using RedNb.Gateway.Domain.Clusters;

namespace RedNb.Gateway.Application.Clusters;


public class ClusterAppService : TreeAppService<Cluster, ClusterAddInputDto, ClusterUpdateInputDto, ClusterOutputDto>,
    IClusterAppService
{
    private readonly ClusterManager _clusterManager;
    private readonly IRepository<Cluster, long> _clusterRepository;
    private readonly IObjectMapper _objectMapper;

    public ClusterAppService(
        IRepository<Cluster, long> clusterRepository,
        IObjectMapper objectMapper,
        ClusterManager clusterManager) :
        base(clusterManager, objectMapper, clusterRepository)
    {
        _clusterRepository = clusterRepository;
        _objectMapper = objectMapper;
        _clusterManager = clusterManager;
    }
}
