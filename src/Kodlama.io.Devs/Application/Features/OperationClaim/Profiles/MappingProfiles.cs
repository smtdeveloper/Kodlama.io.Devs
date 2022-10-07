using Application.Features.OperationClaim.Dto;
using Application.Features.OperationClaim.Model;
using AutoMapper;
using Core.Persistence.Paging;

namespace Application.Features.OperationClaim.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaimListModel, OperationClaimListDto>().ReverseMap();

            CreateMap<IPaginate<Core.Security.Entities.OperationClaim>, OperationClaimListModel>().ReverseMap();

            CreateMap<CreatedOperationClaimDto, Core.Security.Entities.OperationClaim>().ReverseMap();

            CreateMap<DeletedOperationClaimDto, Core.Security.Entities.OperationClaim>().ReverseMap();

            CreateMap<OperationClaimListDto, Core.Security.Entities.OperationClaim>().ReverseMap();



        }
    }
}
