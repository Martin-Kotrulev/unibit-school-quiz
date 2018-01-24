namespace Uniquizbit.Web.Mapping
{
  using System.Collections.Generic;
  using System.Linq;
  using Models;
  using AutoMapper;
  using Data.Models;

  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Domain to resource
      CreateMap<Answer, AnswerResource>()
        .ForMember(a => a.IsRight, opt => opt.Ignore());

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
        .ForMember(qr => qr.Id, opt => opt.Ignore())
        .ForMember(qr => qr.QuizId, opt => opt.Ignore())
        .ForMember(qr => qr.Answers, opt => opt.Ignore());

      CreateMap<AnswerResource, Answer>()
        .ForMember(a => a.Id, opt => opt.Ignore());

      CreateMap<ProgressAnswerResource, ProgressAnswer>(); 
    }
  }
}