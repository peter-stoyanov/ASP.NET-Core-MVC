﻿using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.Models.WordViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Infrastructure.Mapping
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Word, DetailViewModel>();
            CreateMap<Word, WordCreateViewModel>();
            CreateMap<Word, WordEditViewModel>();
            CreateMap<Word, SearchViewModel>();
        }
    }
}
