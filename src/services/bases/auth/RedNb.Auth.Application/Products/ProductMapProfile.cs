using AutoMapper;
using RedNb.Auth.Application.Contracts.Products.Dtos;
using RedNb.Auth.Domain.Products;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Products
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductAddInputDto, Product>();

            CreateMap<ProductUpdateInputDto, Product>();

            CreateMap<Product, ProductOutputDto>();

            CreateMap<Platform, PlatformOutputDto>();
        }
    }
}
