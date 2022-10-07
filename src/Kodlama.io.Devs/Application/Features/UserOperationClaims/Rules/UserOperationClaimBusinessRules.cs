using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;

    public UserOperationClaimBusinessRules(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository)
    {
        _userRepository = userRepository;
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task UserShouldExist(int userId)
    {
        var result = await _userRepository.GetAsync(u => u.Id == userId);
        if (result is null)
            throw new BusinessException("Böyle bir kullanıcı yoktur");
    }

    public async Task OperationClaimShouldExist(int operationClaimId)
    {
        var result = await _operationClaimRepository.GetAsync(o => o.Id == operationClaimId);
        if (result is null)
            throw new BusinessException("Böyle bir Rol yoktur");
    }

    public void UserOperationClaimShouldExistToDelete(UserOperationClaim userOperationClaims)
    {
        if (userOperationClaims is null)
            throw new BusinessException("Kullanıcı rol işlemi talebi mevcut değil");
    }
}

