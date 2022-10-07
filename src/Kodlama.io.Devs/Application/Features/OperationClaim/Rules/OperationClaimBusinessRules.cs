using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaim.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
            => (_operationClaimRepository) = (operationClaimRepository);

        public async Task OperationClaimShouldNotDuplicatedWhenInserted(string role)
        {
            var result = await _operationClaimRepository.GetAsync(o =>
                o.Name.ToLower() == role.ToLower());

            if (result is not null)
                throw new BusinessException("Bu rol zaten var");
        }

        public void OperationClaimShouldExistToDelete(Core.Security.Entities.OperationClaim operationClaim)
        {
            if (operationClaim is null)
                throw new BusinessException("Bu rol mevcut değil");
        }

        public void OperationClaimShouldExistToUpdate(Core.Security.Entities.OperationClaim operationClaim)
        {
            if (operationClaim is null)
                throw new BusinessException("Bu rol mevcut değil");
        }

    }
}