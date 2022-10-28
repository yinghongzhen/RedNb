using AutoMapper;
using RedNb.Auth.Application.Contracts.Posts.Dtos;
using RedNb.Auth.Domain.Offices;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Posts
{
    public class PostMapProfile : Profile
    {
        public PostMapProfile()
        {
            CreateMap<PostAddInputDto, Post>();

            CreateMap<PostUpdateInputDto, Post>();

            CreateMap<Post, PostOutputDto>()
                .ForMember(m => m.TypeStr, m => m.MapFrom(o => o.Type.GetDescription()));
        }
    }
}
