using AutoMapper;
using RedNb.Auth.Application.Contracts.Tenants.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Tenants
{
    public class TenantMapProfile : Profile
    {
        public TenantMapProfile()
        {
            CreateMap<TenantAddInputDto, Tenant>();

            CreateMap<TenantUpdateInputDto, Tenant>();

            CreateMap<Tenant, TenantOutputDto>();
        }
    }
}
