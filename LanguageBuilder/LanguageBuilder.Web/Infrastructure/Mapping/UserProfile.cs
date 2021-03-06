﻿using AutoMapper;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Web.ViewModels.UserViewModels;

namespace LanguageBuilder.Web.Infrastructure.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, User>();

            CreateMap<User, UserProfileViewModel>();
            CreateMap<UserProfileViewModel, User>();

            CreateMap<User, UserEditProfileViewModel>();
            CreateMap<UserEditProfileViewModel, User>();
        }
    }
}
