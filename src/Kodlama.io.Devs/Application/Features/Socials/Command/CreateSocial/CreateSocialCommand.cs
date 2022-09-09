using Application.Features.Socials.Dto;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Command.CreateSocial
{
    public class CreateSocialCommand : IRequest<CreateSocialDto>
    {
        public int DeveloperId { get; set; }
        public string GithubProfile { get; set; }

        public class CreateGitHubProfileCommandHandler : IRequestHandler<CreateSocialCommand, CreateSocialDto>
        {
            private readonly IMapper _mapper;
            private readonly ISocialRepository _socialRepository;

            public CreateGitHubProfileCommandHandler(IMapper mapper, ISocialRepository socialRepository)
            {
                _mapper = mapper;
                _socialRepository = socialRepository;
            }

            public async Task<CreateSocialDto> Handle(CreateSocialCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Social>(request);


                entity = await _socialRepository.AddAsync(entity);

                var createdGitHubProfileDto = _mapper.Map<CreateSocialDto>(entity);

                return createdGitHubProfileDto;
            }
        }

    }
}
