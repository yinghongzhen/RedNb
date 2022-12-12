using AutoMapper;
using RedNb.Auth.Application.Contracts.Employees.Dtos;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Offices;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Employees
{
    public class EmployeeMapProfile : Profile
    {
        public EmployeeMapProfile()
        {
            CreateMap<EmployeeAddInputDto, Employee>();

            CreateMap<EmployeeUpdateInputDto, Employee>();

            CreateMap<Employee, EmployeeOutputDto>();
        }
    }
}
