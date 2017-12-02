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

        //.ForMember(c => c.Trainer, cfg => cfg.MapFrom(c => c.Trainer.UserName))
        //.ForMember(c => c.Students, cfg => cfg.MapFrom(c => c.Students.Count()));
    }
}
