using Application.Features.Socials.Dto;
using Application.Features.Socials.Model;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Queries;

public class GetByIdSocialQuery : IRequest<SocialListDto> , ISecuredRequest
{
    public int SocialId { get; set; }
    public string[] Roles { get; } = { "Admin" };

    public class GetByIdSocialQueryHandler : IRequestHandler<GetByIdSocialQuery, SocialListDto>
    {
        private readonly IMapper _mapper;
        private readonly ISocialRepository _socialRepository;

        public GetByIdSocialQueryHandler(IMapper mapper, ISocialRepository socialRepository)
        {
            _mapper = mapper;
            _socialRepository = socialRepository;
        }

        public async Task<SocialListDto> Handle(GetByIdSocialQuery request, CancellationToken cancellationToken)
        {
            var social = await _socialRepository.GetAsync(p => p.Id == request.SocialId);


            SocialListDto githubProfileListModel = _mapper.Map<SocialListDto>(social);

            return githubProfileListModel;
        }
    }
}

