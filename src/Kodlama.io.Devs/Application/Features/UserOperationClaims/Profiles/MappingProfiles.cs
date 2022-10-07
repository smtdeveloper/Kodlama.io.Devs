using Application.Features.OperationClaim.Dto;
using Application.Features.Technology.Dto;
using Application.Features.Technology.Model;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Core.Security.Entities.UserOperationClaim, CreatedUserOperationClaimDto>()
                .ForMember(c => c.FirstName, opt => opt.MapFrom(c => c.User.FirstName))
                .ForMember(c => c.LastName, opt => opt.MapFrom(c => c.User.LastName))
                .ForMember(c => c.OprationClaimName, opt => opt.MapFrom(c => c.OperationClaim.Name))
                .ReverseMap();

            CreateMap<Core.Security.Entities.UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();

            CreateMap<IPaginate<Core.Security.Entities.UserOperationClaim>, GetListUserOperationClaimModel>().ReverseMap();

            CreateMap<Core.Security.Entities.UserOperationClaim , UserOperationClaimListDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(d => d.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(d => d.User.LastName))
                .ForMember(d => d.OprationClaimName, opt => opt.MapFrom(d => d.OperationClaim.Name))
                .ReverseMap();


        }
    }
}
