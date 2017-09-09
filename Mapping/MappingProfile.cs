using System.Linq;
using App.Controllers.Resources;
using App.Models;
using AutoMapper;

namespace Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to resource
            CreateMap<QuizGroup, QuizGroupResource>()
                .ForMember(qg => qg.Tags, opt => 
                    opt.MapFrom(qg => 
                        qg.Tags.Select(t => t.Tag.Name)));

            // Resource to domain
            CreateMap<QuizGroupResource, QuizGroup>()
                .ForMember(qgr => qgr.CreatedOn, opt => opt.Ignore())
                .ForMember(qgr => qgr.Id, opt => opt.Ignore())
                .ForMember(qgr => qgr.Tags, opt => opt.Ignore());
        }
    }
}