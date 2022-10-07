using Application.Features.OperationClaim.Dto;
using Application.Features.OperationClaim.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.OperationClaim.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } = { "Admin" };

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
            private readonly IMapper _mapper;
            public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
                _mapper = mapper;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request,
                CancellationToken cancellationToken)
            {
                var operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == request.Id);
                _operationClaimBusinessRules.OperationClaimShouldExistToDelete(operationClaim);

                var result = await _operationClaimRepository.DeleteAsync(operationClaim);

                //var deletedOperationClaimDto = new DeletedOperationClaimDto();

                var createdTechnologyDto = _mapper.Map<DeletedOperationClaimDto>(operationClaim);

                return createdTechnologyDto;
            }
        }
    }
}
