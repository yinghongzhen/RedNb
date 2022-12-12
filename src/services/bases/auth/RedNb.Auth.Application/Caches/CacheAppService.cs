using Microsoft.EntityFrameworkCore;
using RedNb.Auth.Application.Contracts.Caches;
using RedNb.Auth.Application.Contracts.Caches.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Hzkj.Auth.Application.Caches
{
    public class CacheAppService : ICacheAppService
    {
        private readonly IObjectMapper _objectMapper;

        public CacheAppService(IObjectMapper objectMapper)
        {
            _objectMapper = objectMapper;
        }

        public Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<List<CacheOutputDto>> GetAllKeyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedOutputDto<CacheOutputDto>> GetPageAsync(CacheGetPageInputDto input)
        {
            throw new NotImplementedException();
        }
    }
}
