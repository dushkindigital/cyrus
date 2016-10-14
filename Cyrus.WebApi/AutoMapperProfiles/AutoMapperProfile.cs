using System;
using AutoMapper;
using Cyrus.Core.DomainModels;
using Cyrus.WebApi.ViewModels;

namespace Cyrus.WebApi.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<Tribe, TribesViewModel>();
            CreateMap<TribeMember, TribeMembersViewModel>();
        }
    }
}