using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.Models.WordViewModels;

namespace LanguageBuilder.Web.Infrastructure.Mapping
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Word, DetailViewModel>();

            CreateMap<Word, WordCreateViewModel>();
            CreateMap<WordCreateViewModel, Word>();

            CreateMap<Word, WordEditViewModel>();
            CreateMap<WordEditViewModel, Word>();

            CreateMap<Word, SearchViewModel>();
        }
    }
}
