using System;
using Cyrus.Core.DomainModels;
using Cyrus.WebApi.ViewModels;
using AutoMapper;

namespace Cyrus.WebApi.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Tribe, TribesViewModel>();
            CreateMap<TribeMember, TribeMembersViewModel>();
        }
    }
}