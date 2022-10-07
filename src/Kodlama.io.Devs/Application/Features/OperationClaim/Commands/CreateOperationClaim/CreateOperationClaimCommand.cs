using Application.Features.OperationClaim.Dto;
using Application.Features.OperationClaim.Rules;
using Application.Features.Technology.Dto;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaim.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>, ISecuredRequest
    {
        public string Role { get; set; }
        public string[] Roles { get; } = { "Admin" };


        public class
            CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
            private readonly IMapper _mapper;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
                _mapper = mapper;   

            }

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request,
                CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimShouldNotDuplicatedWhenInserted(request.Role);

                Core.Security.Entities.OperationClaim operationClaim = new()
                {
                    Name = request.Role.ToLower()
                };

                var result = await _operationClaimRepository.AddAsync(operationClaim);

               // CreatedOperationClaimDto createdOperationClaim = new();

                var createdTechnologyDto = _mapper.Map<CreatedOperationClaimDto>(operationClaim);

                return createdTechnologyDto;
            }
        }
    }
}
