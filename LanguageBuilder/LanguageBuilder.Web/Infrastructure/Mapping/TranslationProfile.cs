using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.DTOs;
using LanguageBuilder.Web.ViewModels.WordViewModels;

namespace LanguageBuilder.Web.Infrastructure.Mapping
{
    public class TranslationProfile : Profile
    {
        public TranslationProfile()
        {
            CreateMap<Translation, TranslationDTO>()
                .ForMember(dest => dest.SourceWord, opts => opts.MapFrom(src => src.SourceWord.Content))
                .ForMember(dest => dest.TargetWord, opts => opts.MapFrom(src => src.TargetWord.Content));

        }
    }
}
