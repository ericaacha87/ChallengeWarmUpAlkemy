using System;
using AutoMapper;
using WarmUpService.DTO;
using WarmUpService.Models;

namespace WarmUpService.Mapper
{

    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Post, PostDTO>();
        }
    }
}