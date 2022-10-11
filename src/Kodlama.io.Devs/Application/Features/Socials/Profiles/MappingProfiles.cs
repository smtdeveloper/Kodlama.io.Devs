using Application.Features.Developers.Dto;
using Application.Features.Socials.Command.CreateSocial;
using Application.Features.Socials.Command.DeleteSocial;
using Application.Features.Socials.Command.UpdateSocial;
using Application.Features.Socials.Dto;
using Application.Features.Socials.Model;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //getlist & getbyid
            CreateMap<GithubProfileListModel, IPaginate<Social>>().ReverseMap();
            CreateMap<GithubProfileListModel, Social>().ReverseMap();
            CreateMap<SocialListDto ,Social >().ReverseMap();
            
            //create
            CreateMap<Social, CreateSocialCommand>().ReverseMap();
            CreateMap<Social, CreateSocialDto>().ReverseMap();

            CreateMap<TokenDto, AccessToken>().ReverseMap();

            //update
            CreateMap<Social, UpdateSocialCommand>().ReverseMap();
            CreateMap<Social, UpdatedSocialDto>().ReverseMap();

            //delete
            CreateMap<Social, DeleteSocialCommand>().ReverseMap();
            CreateMap<Social, UpdatedSocialDto>().ReverseMap();

        }
    }
}
