using Application.Features.OperationClaim.Dto;
using Application.Features.OperationClaim.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaim.Commands.UpdateOperationClaim
{
        public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>, ISecuredRequest
        {
            public string[] Roles { get; } = { "Admin" };

            public Core.Security.Entities.OperationClaim OperationClaim { get; set; }

            public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
            {
                private readonly IOperationClaimRepository _operationClaimRepository;
                private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

                public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository,
                    OperationClaimBusinessRules operationClaimBusinessRules)
                    => (_operationClaimRepository, _operationClaimBusinessRules) =
                        (operationClaimRepository, operationClaimBusinessRules);

                public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request,
                    CancellationToken cancellationToken)
                {
                    var operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == request.OperationClaim.Id);
                    _operationClaimBusinessRules.OperationClaimShouldExistToUpdate(operationClaim);
                    operationClaim.Name = request.OperationClaim.Name.ToLower();
                    var updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);

                    var updatedOperationClaimDto = new UpdatedOperationClaimDto()
                    {
                        Id = updatedOperationClaim.Id,
                        Name = updatedOperationClaim.Name
                    };

                    return updatedOperationClaimDto;
                }
            }
        }
    }
