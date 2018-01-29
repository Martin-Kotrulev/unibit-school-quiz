namespace Uniquizbit.Web.Infrastructure.Mapping
{
  using AutoMapper;
  using Data.Models;
  using Common.Mapping;
  using Models;
  using System;
  using System.Linq;

  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      // Domain to resource
      CreateMap<Answer, AnswerResource>();

      CreateMap<QuizGroup, QuizGroupResource>()
        .ForMember(qg => qg.Tags, opt =>
          opt.MapFrom(qg =>
            qg.Tags.Select(t => t.Tag.Name)));

      CreateMap<QuizGroup, IdNamePairResource>();

      CreateMap<Quiz, QuizResource>()
        .ForMember(q => q.Password, opt => opt.Ignore())
        .ForMember(q => q.Scores, opt => opt.Ignore())
        .ForMember(q => q.Participants, opt => opt.Ignore())
        .ForMember(q => q.Tags, opt =>
          opt.MapFrom(q =>
            q.Tags.Select(t => t.Tag.Name)));

      CreateMap<Quiz, IdNamePairResource>();

      CreateMap<Question, QuestionResource>();

      // Resource to domain
      CreateMap<QuizGroupResource, QuizGroup>()
        .ForMember(qgr => qgr.CreatedOn, opt => opt.Ignore())
        .ForMember(qgr => qgr.Id, opt => opt.Ignore())
        .ForMember(qgr => qgr.Tags, opt => opt.Ignore());

      CreateMap<QuizResource, Quiz>()
        .ForMember(qr => qr.Id, opt => opt.Ignore())
        .ForMember(qr => qr.CreatedOn, opt => opt.Ignore())
        .ForMember(qr => qr.Tags, opt => opt.Ignore())
        .ForMember(qr => qr.Scores, opt => opt.Ignore())
        .ForMember(qr => qr.Participants, opt => opt.Ignore());

      CreateMap<ProgressResource, QuizProgress>()
        .ForMember(pr => pr.UserId, opt => opt.Ignore())
        .ForMember(pr => pr.GivenAnswers, opt => opt.Ignore());

      CreateMap<QuestionResource, Question>()
        .ForMember(qr => qr.QuizId, opt => opt.Ignore());

      CreateMap<AnswerResource, Answer>();

      CreateMap<ProgressAnswerResource, ProgressAnswer>();
      
      var allTypes = AppDomain
          .CurrentDomain
          .GetAssemblies()
          .Where(a => a.GetName().Name.Contains("Uniquizbit"))
          .SelectMany(a => a.GetTypes());

      allTypes
          .Where(t => t.IsClass && !t.IsAbstract && t
              .GetInterfaces()
              .Where(i => i.IsGenericType)
              .Select(i => i.GetGenericTypeDefinition())
              .Contains(typeof(IMapFrom<>)))
          .Select(t => new
          {
            Destination = t,
            Source = t
                  .GetInterfaces()
                  .Where(i => i.IsGenericType)
                  .Select(i => new
                  {
                    Definition = i.GetGenericTypeDefinition(),
                    Arguments = i.GetGenericArguments()
                  })
                  .Where(i => i.Definition == typeof(IMapFrom<>))
                  .SelectMany(i => i.Arguments)
                  .First(),
          })
          .ToList()
          .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

      allTypes
          .Where(t => t.IsClass
              && !t.IsAbstract
              && typeof(ICustomMapping).IsAssignableFrom(t))
          .Select(Activator.CreateInstance)
          .Cast<ICustomMapping>()
          .ToList()
          .ForEach(mapping => mapping.ConfigureMapping(this));
    }
  }
}