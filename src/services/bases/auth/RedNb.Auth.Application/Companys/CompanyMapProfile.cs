using AutoMapper;
using RedNb.Auth.Application.Contracts.Companys.Dtos;
using RedNb.Auth.Domain.Offices;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Companys
{
    public class CompanyMapProfile : Profile
    {
        public CompanyMapProfile()
        {
            CreateMap<CompanyAddInputDto, Company>();

            CreateMap<CompanyUpdateInputDto, Company>();

            CreateMap<Company, CompanyOutputDto>();

            CreateMap<CompanyOutputDto, AntdTreeOutputDto>()
                .ForMember(m => m.Title, m => m.MapFrom(o => o.TreeName))
                .ForMember(m => m.Value, m => m.MapFrom(o => o.Id))
                .ForMember(m => m.Key, m => m.MapFrom(o => o.Id));
        }
    }
}
