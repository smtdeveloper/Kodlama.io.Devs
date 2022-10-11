using Application.Features.ProgrammingLanguage.Dto;
using Application.Features.ProgrammingLanguage.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand : IRequest<CreateProgrammingLanguageDto>, ISecuredRequest
    {
        public string Name { get; set; }

        public string[] Roles { get; } = { "Admin" , "ProgrammingLanguageAdd" };

        public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreateProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _programmingLanguageRepository = programmingLanguageRepository;
                _mapper = mapper;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<CreateProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageBusinessRules.ProgrammingLanguageCannotBeDuplicatedWhenInserted(request.Name);

                var mapperProgrammingLanguage = _mapper.Map<Domain.Entities.ProgrammingLanguage>(request);
                var createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mapperProgrammingLanguage);
                var createdProgrammingLanguageDto = _mapper.Map<CreateProgrammingLanguageDto>(createdProgrammingLanguage);

                return createdProgrammingLanguageDto;
            }
        }
    }
}

