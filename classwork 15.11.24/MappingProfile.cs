using AutoMapper;
using classwork.Core.Models;
using classwork_15._11._24.DTOs;
using System.Text.RegularExpressions;

namespace classwork_15._11._24
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Source, SourceDTO>().ReverseMap();
            CreateMap<Article, ArticleCreateDTO>().ReverseMap();
            CreateMap<Article, ArticleReturnDTO>().ReverseMap();
        }
    }
}
