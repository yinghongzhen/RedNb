using AutoMapper;
using RedNb.Auth.Application.Contracts.Departments.Dtos;
using RedNb.Auth.Domain.Offices;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Departments
{
    public class DepartmentMapProfile : Profile
    {
        public DepartmentMapProfile()
        {
            CreateMap<DepartmentAddInputDto, Department>();

            CreateMap<DepartmentUpdateInputDto, Department>();

            CreateMap<Department, DepartmentOutputDto>();

            CreateMap<DepartmentOutputDto, AntdTreeOutputDto>()
                .ForMember(m => m.Title, m => m.MapFrom(o => o.TreeName))
                .ForMember(m => m.Value, m => m.MapFrom(o => o.Id))
                .ForMember(m => m.Key, m => m.MapFrom(o => o.Id));
        }
    }
}
