using Application.Features.ProgrammingLanguage.Dtos;
using Application.Features.ProgrammingLanguage.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.ProgrammingLanguage.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommand : IRequest<CreateProgrammingLanguageDto>
{
    public string Name { get; set; }

    public class
        CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand,
            CreateProgrammingLanguageDto>
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
            _programmingLanguageBusinessRules.ProgrammingLanguageNameCannotBeNull(request.Name);
            await _programmingLanguageBusinessRules.ProgrammingLanguageCannotBeDuplicatedWhenInserted(request.Name);

            var mapperProgrammingLanguage = _mapper.Map<Domain.Entities.ProgrammingLanguage>(request);
            var createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mapperProgrammingLanguage);
            var createdProgrammingLanguageDto = _mapper.Map<CreateProgrammingLanguageDto>(createdProgrammingLanguage);
            
            return createdProgrammingLanguageDto;
        }
    }
}
