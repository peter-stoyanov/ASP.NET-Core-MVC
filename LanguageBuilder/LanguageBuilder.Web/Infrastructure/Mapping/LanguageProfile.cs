using AutoMapper;
using LanguageBuilder.Services.Models;
using LanguageBuilder.Web.Areas.Admin.Models;

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
