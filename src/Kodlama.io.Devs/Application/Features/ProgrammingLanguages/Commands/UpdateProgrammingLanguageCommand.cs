using Application.Features.ProgrammingLanguage.Dto;
using Application.Features.ProgrammingLanguage.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands
{
    public class UpdateProgrammingLanguageCommand : IRequest<CreateProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class CreateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, CreateProgrammingLanguageDto>
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

            public async Task<CreateProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {

                await _programmingLanguageBusinessRules.ProgrammingLanguageShouldBeExist(request.Id);
                
                await _programmingLanguageBusinessRules.ProgrammingLanguageCannotBeDuplicatedWhenInserted(request.Name);

                var programmingLanguage = await _programmingLanguageRepository.GetAsync(w => w.Id == request.Id);

                programmingLanguage.Name = request.Name;

                var updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);
                var mappedProgrammingLanguageDto = _mapper.Map<CreateProgrammingLanguageDto>(updatedProgrammingLanguage);

                return mappedProgrammingLanguageDto;
            }
        }
    }
}
