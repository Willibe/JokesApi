using AutoMapper;
using JokesApi.DTOs.Author;
using JokesApi.DTOs.Category;
using JokesApi.DTOs.Joke;
using JokesApi.DTOs.Language;
using JokesApi.Mapper.ValueProviders;
using JokesApi.Mapper.ValueProviders.Joke;
using JokesApi.Models;

namespace JokesApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {


            CreateMap<AuthorModel, AuthorCreate>().ReverseMap()
                .ForMember(model => model.Name, create => create.MapFrom<AuthorCreateIdResolver>());
            CreateMap<AuthorModel, AuthorUpdate>().ReverseMap()
                .ForMember(model => model.Name, update => update.MapFrom<AuthorUpdateIdResolver>());
            CreateMap<AuthorView, AuthorModel>().ReverseMap();



            CreateMap<CategoryModel, CategoryCreate>().ReverseMap()
                .ForMember(model => model.Name, create => create.MapFrom<CategoryCreateIdResolver>());
            CreateMap<CategoryModel, CategoryUpdate>().ReverseMap()
                .ForMember(model => model.Name, create => create.MapFrom<CategoryUpdateIdResolver>());
            CreateMap<CategoryView, CategoryModel>().ReverseMap();


            CreateMap<LanguageModel, LanguageCreate>().ReverseMap()
                .ForMember(model => model.Name, create => create.MapFrom<LanguageCreateIdResolver>());
            CreateMap<LanguageModel, LanguageUpdate>().ReverseMap()
                .ForMember(model => model.Name, create => create.MapFrom<LanguageUpdateIdResolver>());
            CreateMap<LanguageView, LanguageModel>().ReverseMap();


            CreateMap<JokeModel, JokeCreate>().ReverseMap()
                .ForMember(model => model.AuthorId, create => create.MapFrom<JokeModelAuthorIdMemberResolver, string>(src => src.AuthorId))
                .ForMember(model => model.CategoryId, create => create.MapFrom<JokeModelCategoryIdMemberResolver, string>(src => src.CategoryId))
                .ForMember(model => model.LanguageId, create => create.MapFrom<JokeModelLanguageIdMemberResolver, string>(src => src.LanguageId));

            CreateMap<JokeModel, JokeUpdate>().ReverseMap()
                .ForMember(model => model.AuthorId, create => create.MapFrom<JokeModelAuthorIdMemberResolver, string>(src => src.AuthorId))
                .ForMember(model => model.CategoryId, create => create.MapFrom<JokeModelCategoryIdMemberResolver, string>(src => src.CategoryId))
                .ForMember(model => model.LanguageId, create => create.MapFrom<JokeModelLanguageIdMemberResolver, string>(src => src.LanguageId));

            CreateMap<JokeModel, JokeUpdatePartially>().ReverseMap()
                .ForMember(model => model.Id, create => create.MapFrom((src, dest) => dest.Id))
                .ForMember(model => model.Deleted, create => create.MapFrom((src) => false))
                .ForMember(model => model.Description, create => create.MapFrom((src, dest) => src.Description ?? dest.Description))
                .ForMember(model => model.AuthorId, create => create.MapFrom<UpdatePartiallyAuhorIdResolver, string?>(src => src.AuthorId))
                .ForMember(model => model.CategoryId, create => create.MapFrom<UpdatePartiallyCategoryIdResolver, string?>(src => src.CategoryId))
                .ForMember(model => model.LanguageId, create => create.MapFrom<UpdatePartiallyLanguageIdResolver, string?>(src => src.LanguageId));

            CreateMap<JokeView, JokeModel>().ReverseMap()
                .ForMember(model => model.Author, create => create.MapFrom<JokeViewAuthorMemberResolver, int>(src => src.AuthorId))
                .ForMember(model => model.Category, create => create.MapFrom<JokeViewCategoryMemberResolver, int>(src => src.CategoryId))
                .ForMember(model => model.Language, create => create.MapFrom<JokeViewLanguageMemberResolver, int>(src => src.LanguageId));



        }
    }
}
