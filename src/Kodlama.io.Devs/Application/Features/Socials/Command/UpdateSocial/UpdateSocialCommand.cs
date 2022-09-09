using Application.Features.Socials.Dto;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Socials.Command.UpdateSocial
{
    public class UpdateSocialCommand : IRequest<UpdatedSocialDto>
    {
        public int Id { get; set; }
        public string GithubProfile { get; set; }


        public class UpdateSocialCommandQuery : IRequestHandler<UpdateSocialCommand, UpdatedSocialDto>
        {

            private readonly IMapper _mapper;
            private readonly ISocialRepository _socialRepository;

            public UpdateSocialCommandQuery(IMapper mapper, ISocialRepository socialRepository)
            {
                _mapper = mapper;
                _socialRepository = socialRepository;
            }

            public async Task<UpdatedSocialDto> Handle(UpdateSocialCommand request, CancellationToken cancellationToken)
            {
                var entity = await _socialRepository.GetAsync(s => s.Id == request.Id);

                entity = await _socialRepository.UpdateAsync(_mapper.Map(request, entity));

                var mappedEntityDto = _mapper.Map<UpdatedSocialDto>(entity);

                return mappedEntityDto;
            }
        }


    }
}
