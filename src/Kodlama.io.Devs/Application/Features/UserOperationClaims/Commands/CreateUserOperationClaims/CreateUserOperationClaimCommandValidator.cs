using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;

public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
{
    public CreateUserOperationClaimCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
        RuleFor(u => u.OperationClaimId).NotEmpty();
    }
}

