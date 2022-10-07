using Application.Features.OperationClaim.Dto;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;
public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimDto>, ISecuredRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
    public string[] Roles { get; } = { "Admin" };

    public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
        private readonly IMapper _mapper;

        public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules, IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            _mapper = mapper;
        }

        public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            await _userOperationClaimBusinessRules.UserShouldExist(request.UserId);
            await _userOperationClaimBusinessRules.OperationClaimShouldExist(request.OperationClaimId);

            UserOperationClaim userOperationClaim = new()
            {
                UserId = request.UserId,
                OperationClaimId = request.OperationClaimId
                
     
            };

            var createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(userOperationClaim);

            // var createdUserOperationClaimDto = new CreatedOperationClaimDto();

            var createdTechnologyDto = _mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);

            return createdTechnologyDto;
        }
    }
}

