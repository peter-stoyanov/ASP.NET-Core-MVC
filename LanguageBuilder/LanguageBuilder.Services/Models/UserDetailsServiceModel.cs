using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Mapping;

namespace LanguageBuilder.Services.Models
{
    public class UserDetailsServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public void ConfigureMapping(Profile mapper)
             => mapper
                .CreateMap<User, UserDetailsServiceModel>();

        //.ForMember(c => c.Subscription, cfg => cfg.MapFrom(u => u.Subscription));
    }
}
