using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Services.Models
{
    public class LanguageListingServiceModel : IMapFrom<Language>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WordsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
             => mapper
                .CreateMap<Language, LanguageListingServiceModel>()
                .ForMember(l => l.WordsCount, cfg => cfg.MapFrom(l => (long)l.Words.Count));
    }
}
