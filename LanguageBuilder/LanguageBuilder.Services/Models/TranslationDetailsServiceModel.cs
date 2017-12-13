using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Mapping;

namespace LanguageBuilder.Services.Models
{
    public class TranslationDetailsServiceModel : IMapFrom<Translation>, IHaveCustomMapping
    {
        public void ConfigureMapping(Profile mapper)
             => mapper
                .CreateMap<Translation, TranslationDetailsServiceModel>();
    }
}
