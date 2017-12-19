using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Web.Areas.Admin.Models;
using LanguageBuilder.Web.DTOs;
using LanguageBuilder.Web.ViewModels.WordViewModels;

namespace LanguageBuilder.Web.Infrastructure.Mapping
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<LanguageListingServiceModel, LanguageListingViewModel>();
        }
    }
}
